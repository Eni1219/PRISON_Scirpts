using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// エレベーターの起動ボタンを制御するクラス。
/// プレイヤーが踏むとボタンが押し込まれ、イベントを発火します。
/// </summary>
public class ElevatorButton : MonoBehaviour
{
    /// <summary>関連するエレベーターコンポーネント</summary>
    private Elevator elevator;

    /// <summary>ボタンの元の位置</summary>
    private Vector3 originalPos;

    /// <summary>ボタンの押し込み深さ</summary>
    public float pressDepth = .2f;

    /// <summary>ボタンの押し込みアニメーション速度</summary>
    public float pressSpeed = 3f;

    /// <summary>ボタンが押されているかどうか</summary>
    private bool isPressed = false;

    /// <summary>ボタンが押された時に発火するイベント</summary>
    public UnityEvent onPressed;

    /// <summary>
    /// 初期化処理。親のElevatorコンポーネントと初期位置を取得します。
    /// </summary>
    void Start()
    {
        elevator = GetComponentInParent<Elevator>();
        originalPos = transform.localPosition;
    }

    /// <summary>
    /// 毎フレームの更新処理。ボタンの押し込みアニメーションを処理します。
    /// </summary>
    void Update()
    {
        // 押されている場合は下に移動、そうでなければ元の位置に戻る
        Vector3 targetPos = originalPos + (isPressed ? Vector3.down * pressDepth : Vector3.zero);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * pressSpeed);
    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// ボタンを押し込み、イベントを発火します。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
            onPressed.Invoke();
        }
    }

    /// <summary>
    /// プレイヤーがトリガー領域から出た時の処理。
    /// ボタンを元の状態に戻します。
    /// </summary>
    /// <param name="other">トリガーから離れたコライダー</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
            // プレイヤーの親子関係を解除
            other.transform.SetParent(null);
        }
    }
}

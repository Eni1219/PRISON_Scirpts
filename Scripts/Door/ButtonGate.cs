using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボタンで開閉するゲート（扉）を管理するクラス。
/// 外部からGateOpen()を呼び出すことで、指定の位置まで移動します。
/// </summary>
public class ButtonGate : MonoBehaviour
{
    /// <summary>ゲートが開いた時の目標位置</summary>
    public Transform openPoint;

    /// <summary>ゲートの移動速度</summary>
    public float speed = 2f;

    /// <summary>ゲートが開いているかどうか</summary>
    private bool isOpen = false;

    /// <summary>
    /// 毎フレームの更新処理。ゲートが開いている場合、目標位置に向かって移動します。
    /// </summary>
    private void Update()
    {
        if (!isOpen) return;
        // ドアオープンのSEを再生
        AudioManager.instance.Play("DoorOpen");
        // 目標位置に向かって移動
        transform.position = Vector2.MoveTowards(transform.position, openPoint.position, speed * Time.deltaTime);
    }

    /// <summary>
    /// ゲートを開きます。
    /// </summary>
    public void GateOpen()
    {
        isOpen = true;
    }
}

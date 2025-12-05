using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがインタラクトできるドアを管理するクラス。
/// プレイヤーが範囲内でキーを押すと指定の目的地に移動します。
/// </summary>
public class Door : MonoBehaviour
{
    /// <summary>移動先の目的地ID（DoorSystemに登録されているID）</summary>
    public string destinationId;

    /// <summary>ドアを開けるためのキー</summary>
    public KeyCode interactKey = KeyCode.F;

    /// <summary>表示するプロンプトメッセージ</summary>
    public string prompt = "Fで開く";

    /// <summary>インタラクト可能時に表示するUI</summary>
    public GameObject F;

    /// <summary>プレイヤーがインタラクト可能かどうか</summary>
    bool _can;

    /// <summary>範囲内のプレイヤーのTransform</summary>
    Transform _player;

    /// <summary>
    /// 初期化処理。インタラクトUIを非表示にします。
    /// </summary>
    void Start()
    {
        Debug.Log($"[Door] Start - destinationId = '{destinationId}'");
        F.SetActive(false);
    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _can = true;
        _player = other.transform;
        // インタラクトUIを表示
        F.SetActive(true);
    }

    /// <summary>
    /// プレイヤーがトリガー領域から出た時の処理。
    /// </summary>
    /// <param name="other">トリガーから離れたコライダー</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _can = false;
        _player = null;
        // インタラクトUIを非表示
        F.SetActive(false);
    }

    /// <summary>
    /// 毎フレームの更新処理。インタラクト入力を監視します。
    /// </summary>
    private void Update()
    {
        // インタラクト可能状態でキーが押された場合、目的地に移動
        if (_can && Input.GetKeyDown(interactKey) && _player)
        {
            var ok = DoorSystem.Go(destinationId, _player);
        }
    }
}

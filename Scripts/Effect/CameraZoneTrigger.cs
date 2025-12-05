using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが特定のエリアに入った時にカメラを切り替えるトリガークラス。
/// カットシーンやイベント時の固定カメラ演出に使用します。
/// </summary>
public class CameraZoneTrigger : MonoBehaviour
{
    /// <summary>トリガー時に非表示にするUI</summary>
    [SerializeField] private GameObject ui;

    /// <summary>切り替え先の固定カメラ</summary>
    [SerializeField] private CinemachineVirtualCamera fixCam;

    /// <summary>アクティブ時のカメラ優先度</summary>
    [SerializeField] private int highPriority = 20;

    /// <summary>非アクティブ時のカメラ優先度</summary>
    [SerializeField] private int lowPriority = 5;

    /// <summary>トリガー時に走り始めるNPC</summary>
    [SerializeField] private NPCRunner npc;

    /// <summary>既にトリガーが発動したかどうか</summary>
    private bool hasTriggered = false;

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// カメラを切り替え、NPCの走りを開始します。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 既に発動済みまたはプレイヤー以外は無視
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        // カメラの優先度を上げて切り替え
        if (fixCam != null)
        {
            fixCam.Priority = highPriority;
            ui.SetActive(false);
        }
        // NPCの走りを開始
        if (npc != null)
        {
            npc.StartRunning();
        }
    }
}

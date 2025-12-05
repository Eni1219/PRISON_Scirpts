using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エンカウンター（戦闘）を開始するトリガークラス。
/// プレイヤーが特定エリアに入ると戦闘を開始します。
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class EncounterTrigger : MonoBehaviour
{
    /// <summary>関連するエンカウンターマネージャー</summary>
    public EncounterManager encounterManager;

    /// <summary>既にトリガーが発動したかどうか</summary>
    private bool isTriggered = false;

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// エンカウンターを開始します。
    /// </summary>
    /// <param name="collision">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 既に発動済みの場合は何もしない
        if (isTriggered) return;
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            if (encounterManager != null)
                encounterManager.StartEncounter();
        }
    }

    /// <summary>
    /// トリガーの発動状態をリセットします。
    /// プレイヤー死亡時のリスポーン処理で使用します。
    /// </summary>
    public void ResetTrigger()
    {
        isTriggered = false;
        Debug.Log("True Now");
    }
}
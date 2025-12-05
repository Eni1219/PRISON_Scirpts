using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// カットシーン（ムービー演出）を開始するトリガークラス。
/// プレイヤーが特定エリアに入るとTimelineを再生し、UIを非表示にします。
/// </summary>
public class CutSceneTrigger : MonoBehaviour
{
    /// <summary>再生するTimelineのPlayableDirector</summary>
    public PlayableDirector director;

    /// <summary>カットシーン中に非表示にするHPパネル</summary>
    public GameObject HPPanel;

    /// <summary>カットシーン中に非表示にするプレイヤー</summary>
    public GameObject player;

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// プレイヤーとUIを非表示にしてカットシーンを再生します。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player came");
            // プレイヤーとUIを非表示
            player.SetActive(false);
            HPPanel.SetActive(false);
            // カットシーン再生開始
            director.Play();
        }
    }
}

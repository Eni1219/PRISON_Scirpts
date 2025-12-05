using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボス戦エリアに入った時にボス戦用BGMを再生するトリガークラス。
/// プレイヤーがトリガー領域に入ると「BossFightBgm」を再生します。
/// </summary>
public class BossFightBgm : MonoBehaviour
{
    /// <summary>BGMが現在再生中かどうか</summary>
    bool isPlaying = false;

    /// <summary>既にトリガーが発動したかどうか（一度だけ発動）</summary>
    bool hasPlayedTrigger = false;

    /// <summary>
    /// 初期化処理（現在は空実装）
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時にボス戦BGMを再生します。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤータグを持ち、まだ発動していない場合のみ処理
        if (other.CompareTag("Player") && !hasPlayedTrigger)
        {
            hasPlayedTrigger = true;
            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("BossFightBgm");
            }
            isPlaying = true;
        }
    }

    /// <summary>
    /// トリガーの発動状態をリセットします。
    /// プレイヤー死亡時のリスポーン処理などで使用されます。
    /// </summary>
    public void ResetTrigger()
    {
        hasPlayedTrigger = false;
    }

}

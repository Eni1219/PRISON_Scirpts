using UnityEngine;

/// <summary>
/// プレイヤーが特定のエリアに入った時にBGMを再生または停止するトリガークラス。
/// Collider2Dをトリガーモードに設定して使用します。
/// </summary>
public class BgmTrigger : MonoBehaviour
{
    /// <summary>
    /// トリガー時に実行するアクションの種類を定義します。
    /// </summary>
    public enum TriggerAction
    {
        /// <summary>BGMを再生する</summary>
        Play,
        /// <summary>BGMを停止する</summary>
        Stop
    }

    /// <summary>操作対象のBGM名（AudioManagerに登録されている名前）</summary>
    [Header("BGM Settings")]
    [SerializeField] private string bgmName;

    /// <summary>トリガー時に実行するアクション（Play または Stop）</summary>
    [SerializeField] private TriggerAction action;

    /// <summary>既にトリガーが発動したかどうかのフラグ（一度だけ発動）</summary>
    private bool hasBeenTriggered = false;

    /// <summary>
    /// プレイヤーがトリガー領域に入った時にBGMを操作します。
    /// 一度発動すると再発動しません。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤータグを持つオブジェクトかつ未発動の場合のみ処理
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true; 

            // AudioManagerが存在しない場合は処理を中断
            if (AudioManager.instance == null) return;

            // 設定されたアクションに応じてBGMを操作
            switch (action)
            {
                case TriggerAction.Play:
                    AudioManager.instance.Play(bgmName);
                    break;
                case TriggerAction.Stop:
                    AudioManager.instance.Stop(bgmName);
                    break;
            }
        }
    }
}
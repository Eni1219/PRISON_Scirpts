using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 休憩ポイント（セーブポイント）を管理するクラス。
/// プレイヤーが近くでインタラクトすると、リスポーン地点を設定し、HP回復を行います。
/// </summary>
public class RestPoint : MonoBehaviour
{
    /// <summary>インタラクトに使用するキー</summary>
    public KeyCode interactKey = KeyCode.F;

    /// <summary>休憩時のフェード時間（秒）</summary>
    public float fade = 0.35f;

    /// <summary>休憩の保持時間（秒）</summary>
    public float restHold = 2f;

    /// <summary>休憩時にHPを全回復するかどうか</summary>
    public bool healOnRest = true;

    /// <summary>プレイヤーがインタラクト可能かどうか</summary>
    bool can;

    /// <summary>範囲内のプレイヤー参照</summary>
    private Player player;

    /// <summary>
    /// コンポーネントリセット時にコライダーをトリガーに設定します。
    /// </summary>
    void Reset() { GetComponent<Collider2D>().isTrigger = true; }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// </summary>
    /// <param name="c">トリガーに接触したコライダー</param>
    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        can = true;
        player = c.GetComponent<Player>();
    }

    /// <summary>
    /// プレイヤーがトリガー領域から出た時の処理。
    /// </summary>
    /// <param name="c">トリガーから離れたコライダー</param>
    void OnTriggerExit2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        can = false;
        player = null;
    }

    /// <summary>
    /// 毎フレームの更新処理。インタラクト入力を監視します。
    /// </summary>
    void Update()
    {
        if (!can || player == null) return;

        // インタラクトキーが押されたら休憩処理を実行
        if (Input.GetKeyDown(interactKey))
        {
            // リスポーン地点を現在の位置に設定
            GameManager.instance.SetRespawnPoint(transform.position);
            // プレイヤーを休憩状態に
            player.StartRest(fade, restHold, healOnRest);
            // 設定に応じてHP全回復と回復回数リセット
            if (healOnRest)
            {
                player.stats.HealToFull();
                player.RestoreAllHeals();
            }
            // 全ての敵を復活させる
            EnemyRespawnUtil.RestoreAll();
        }
    }
}

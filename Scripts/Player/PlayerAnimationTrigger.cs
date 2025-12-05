using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションイベントを処理するクラス。
/// アニメーションの特定フレームで攻撃判定や効果音再生を行います。
/// </summary>
public class PlayerAnimationTrigger : MonoBehaviour
{
    /// <summary>親オブジェクトのPlayerコンポーネントを取得</summary>
    private Player player => GetComponentInParent<Player>();

    /// <summary>
    /// アニメーションイベントから呼ばれる汎用トリガー。
    /// 現在の状態にアニメーション完了を通知します。
    /// </summary>
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    /// <summary>
    /// 回復アニメーションイベント時に呼ばれる処理。
    /// 回復回数があればHPを回復します。
    /// </summary>
    private void HealTrigger()
    {
        if (player.TryUseHeal()) // 回復可能かチェック
        {
            player.stats.Heal(5); // HPを回復
            player.HealEffect();  // 回復エフェクト再生
        }
    }

    /// <summary>
    /// 攻撃時の効果音を再生します。
    /// </summary>
    /// <param name="index">攻撃のインデックス（2の場合はAttack2を再生）</param>
    public void PlayAttackSE(int index)
    {
        if (index == 2)
        {
            AudioManager.instance.Play("Attack2");
        }
        else
        {
            AudioManager.instance.Play("Attack1");
        }
    }

    /// <summary>
    /// 空中攻撃時の効果音を再生します。
    /// </summary>
    public void PlayAirAttackSE()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.Play("AirAttack");
    }

    /// <summary>
    /// 攻撃判定を行います（アニメーションイベントから呼ばれます）。
    /// 攻撃範囲内の敵やBreakableオブジェクトにダメージを与えます。
    /// </summary>
    private void AttackTrigger()
    {
        // 攻撃範囲内のコライダーを取得
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // 敵にダメージを与える
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);
            }

            // 壊せるオブジェクトにヒットした場合の処理
            var breakable = hit.GetComponentInParent<IBreakable>();
            if (breakable != null)
                breakable.TakeHit(1, new Vector2(player.facingDir, 0));
        }
    }
}

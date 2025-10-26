using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    // アニメーションイベントから呼ばれる汎用トリガー
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    // 回復アニメーションイベント時に呼ばれる処理
    private void HealTrigger()
    {
        if (player.TryUseHeal()) // 回復可能かチェック
        {
            player.stats.Heal(5); // HPを回復
            player.HealEffect();  // 回復エフェクト再生
        }
    }

    // 攻撃時の効果音を再生（indexによって分岐）
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

    // 空中攻撃時の効果音を再生
    public void PlayAirAttackSE()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.Play("AirAttack");
    }

    // 攻撃判定を行う（アニメーションイベントから呼ばれる）
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

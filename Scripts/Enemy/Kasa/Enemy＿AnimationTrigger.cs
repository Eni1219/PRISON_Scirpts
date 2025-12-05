using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のアニメーションイベントを処理するクラス。
/// 攻撃判定や効果音再生をアニメーションから呼び出します。
/// </summary>
public class Enemy＿AnimationTrigger : MonoBehaviour
{
    /// <summary>このゲームオブジェクトのEnemyStatsコンポーネント</summary>
    private EnemyStats stats => GetComponent<EnemyStats>();

    /// <summary>親オブジェクトのEnemyコンポーネント</summary>
    private Enemy enemy => GetComponentInParent<Enemy>();

    /// <summary>
    /// アニメーション完了トリガー。現在の状態に完了を通知します。
    /// </summary>
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    /// <summary>
    /// 攻撃判定トリガー。攻撃範囲内のプレイヤーにダメージを与えます。
    /// </summary>
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats _target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(_target);
            }
        }
    }

    /// <summary>
    /// 特殊攻撃トリガー。敵の特殊攻撃処理を呼び出します。
    /// </summary>
    private void SpecialAttackTrigger()
    {
        enemy.AnimationSpecialAttackTrigger();
    }

    /// <summary>
    /// ボス投射物トリガー。ボスの投射物攻撃処理を呼び出します。
    /// </summary>
    private void BossProjectileTrigger()
    {
        enemy.BossProjectileAttackTrigger();
    }

    /// <summary>ボス攻撃1のSEを再生します。</summary>
    public void BossAttack1SE()
    {
        AudioManager.instance.Play("BossAttack1");
    }

    /// <summary>ボス攻撃2のSEを再生します。</summary>
    public void BossAttack2SE()
    {
        AudioManager.instance.Play("BossAttack2");
    }

    /// <summary>ボス魔法攻撃のSEを再生します。</summary>
    public void BossMagic()
    {
        AudioManager.instance.Play("BossMagic");
    }

    /// <summary>笠敵の攻撃SEを再生します。</summary>
    public void KasaAttack()
    {
        AudioManager.instance.Play("Enemy1Attack");
    }

    /// <summary>ネクロマンサー攻撃のSEを再生します。</summary>
    public void MancerAttack()
    {
        AudioManager.instance.Play("MancerAttack");
    }

    /// <summary>カメラシェイクを発動します。</summary>
    public void ShakeCamera() => CameraShaker.instance.GenerateSmallShake();

    /// <summary>死亡アニメーション完了を通知します。</summary>
    public void DeathFinished() => stats?.OnDeathAnimationFinished();

    /// <summary>カウンター攻撃ウィンドウを開きます。</summary>
    protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    /// <summary>カウンター攻撃ウィンドウを閉じます。</summary>
    protected void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}

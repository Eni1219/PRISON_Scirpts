using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy£ßAnimationTrigger : MonoBehaviour
{
    private EnemyStats stats=>GetComponent<EnemyStats>();
    private Enemy enemy=>GetComponentInParent<Enemy>(); 

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats _target=hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(_target);
            }
        }
    }
    private void SpecialAttackTrigger()
    {
        enemy.AnimationSpecialAttackTrigger();
    }
    private void BossProjectileTrigger()
    {
        enemy.BossProjectileAttackTrigger();
    }
    public void BossAttack1SE()
    {
        AudioManager.instance.Play("BossAttack1");
    }
    public void BossAttack2SE()
    {
        AudioManager.instance.Play("BossAttack2");
    }
    public void BossMagic()
    {
        AudioManager.instance.Play("BossMagic");
    }
    public void KasaAttack()
    {
        AudioManager.instance.Play("Enemy1Attack");
    }
    public void MancerAttack()
    {
        AudioManager.instance.Play("MancerAttack");
    }
    public void ShakeCamera() => CameraShaker.instance.GenerateSmallShake();
    public void DeathFinished() => stats?.OnDeathAnimationFinished();
    protected void OpenCounterWindow()=>enemy.OpenCounterAttackWindow();
    protected void CloseCounterWindow()=>enemy.CloseCounterAttackWindow();
}

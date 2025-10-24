using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
   private Player player=>GetComponentInParent<Player>();
   private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void HealTrigger()
    {
        if(player.TryUseHeal())
        {
        player.stats.Heal(5);
        player.HealEffect();

        }
    }
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
    public void PlayAirAttackSE()
    {
        if(AudioManager.instance!=null)
        AudioManager.instance.Play("AirAttack");
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target= hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);
            }
            var breakable = hit.GetComponentInParent<IBreakable>();
            if (breakable != null)
                breakable.TakeHit(1, new Vector2(player.facingDir, 0));
        }
    }
}

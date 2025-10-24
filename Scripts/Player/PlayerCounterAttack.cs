using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttack : EntityState
{
    public PlayerCounterAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0,0);
        AudioManager.instance.Play("CounterSeccessful");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<FireBall_Controller>() != null)
            {
                hit.GetComponent<FireBall_Controller>().FlipFireBall();
                stateTimer = 2f;
                player.anim.SetBool("SuccessfulCounterAttack", true);
            }
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 2f;
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }
        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
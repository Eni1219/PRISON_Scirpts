using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PrimaryAttack :EntityState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    private float inputBufferTimer = .1f;
    public Player_PrimaryAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        stateTimer = .1f;
        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1.2f;
        player.StartCoroutine(DetermineAttackDir());
        
    }
    private IEnumerator DetermineAttackDir()
    {
        yield return null;

        #region Attack Direction
        float attackDir;
        if (xInput != 0)
        {
            attackDir = Mathf.Sign(xInput);
            if (xInput != player.facingDir)
                player.Flip();
        }
        else
        {
            attackDir = player.facingDir;
        }

        #endregion
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

    }

    

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .2f);
        player.anim.speed = 1;
        comboCounter++;
        lastTimeAttacked=Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.ZeroVelocity();
        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}

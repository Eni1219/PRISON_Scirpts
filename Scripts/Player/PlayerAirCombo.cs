using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirCombo : EntityState
{
    private int airComboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerAirCombo(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (airComboCounter > 1 || Time.time >= lastTimeAttacked + comboWindow)
            airComboCounter = 0;
        stateTimer = .1f;
        player.anim.SetInteger("AirCombo", airComboCounter);
        player.anim.speed = 1.2f;
        #region Attack Direction
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;
        #endregion
        player.SetVelocity(player.attackMovement[airComboCounter].x * attackDir, player.attackMovement[airComboCounter].y);

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .2f);
        player.anim.speed = 1;
        airComboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        { 
            player.ZeroVelocity();
        }

        if (triggerCalled)
            stateMachine.ChangeState(player.airState);
    }
}

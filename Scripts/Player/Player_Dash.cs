using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : EntityState
{
    public Player_Dash(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.Play("Dash");
        stateTimer =player.dashDuration;

       
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashDir*player.dashSpeed,0);

        if ((stateTimer<0))
        stateMachine.ChangeState(player.idleState);
       

    }
}

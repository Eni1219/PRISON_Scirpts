using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallJump : EntityState
{

    public Player_WallJump(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        player.SetVelocity(5*-player.facingDir,player.jumpForce);
        AudioManager.instance.Play("Jump");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
            stateMachine.ChangeState(player.airState);
    }
}

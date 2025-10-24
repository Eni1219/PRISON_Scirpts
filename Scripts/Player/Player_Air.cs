using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Air : EntityState
{
    public Player_Air(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
  
    }

    public override void Update()
    {
        base.Update();
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        if(player.isWallDetected())
        {
            if (player.canWallSlide)
        stateMachine.ChangeState(player.wallSlideState);
        }
        if(Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.airCombo);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.canDoubleJump && ! player.doubleJumpUsed)
            {
                player.doubleJumpUsed = true;
                stateMachine.ChangeState(player.jumpState);
                AudioManager.instance.Play("Jump");
            }
        }
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f* xInput, rb.velocity.y);
    }
}

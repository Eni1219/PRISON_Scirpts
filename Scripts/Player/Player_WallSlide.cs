using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_WallSlide : EntityState
{
    public Player_WallSlide(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.sr.flipX = true;
        rb.gravityScale = 0.5f;
    }
    public override void Exit()
    {
        base.Exit();
        player.sr.flipX = false;
        rb.gravityScale = 3f;
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .8f);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
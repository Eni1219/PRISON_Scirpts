using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : EntityState
{
    public Player_Jump(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity=new Vector2 (rb.velocity.x,player.jumpForce);
        AudioManager.instance.Play("Jump");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}

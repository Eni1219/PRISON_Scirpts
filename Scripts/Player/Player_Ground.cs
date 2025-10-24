using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ground : EntityState
{
  
    public Player_Ground(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.K)&&player.canCounter)
            stateMachine.ChangeState(player.counterAttack);
        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.primaryAttack);
        if(!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
        if(Input.GetKeyDown(KeyCode.Space)&&player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.healState);
    }
}

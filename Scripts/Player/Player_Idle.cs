using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : Player_Ground
{
    public Player_Idle(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine,animBoolName)
    { 
    
    }
    public override void Enter()
    {
        base.Enter();
        player.Landed();
        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (xInput!=0&&!player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}

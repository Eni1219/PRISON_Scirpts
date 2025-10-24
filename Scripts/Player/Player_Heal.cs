using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Heal : EntityState
{

    public Player_Heal(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.Play("Heal");
    }
    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        triggerCalled = true;
    }
    
}

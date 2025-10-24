using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : EntityState
{
    public Player_Death(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        player.gameObject.layer = LayerMask.NameToLayer("Dead");
        AudioManager.instance.Play("PlayerDeath");
        UIManager.instance.ShowDeathUI();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : Player_Ground
{
    
    public Player_Move(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    
    public override void Enter()
    {
        base.Enter();
     if(player.footStepSE&&player.audioSource)
        {
            player.audioSource.clip = player.footStepSE;
            player.audioSource.loop = true;
            player.audioSource.Play();
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        if(player.audioSource&&player.audioSource.isPlaying)
            player.audioSource.Stop();
    }
    public override void Update()
    {
        base.Update();
        player.SetVelocity(xInput*player.moveSpeed ,rb.velocity.y);
           
        if (xInput==0)
            stateMachine.ChangeState(player.idleState);
    }
}

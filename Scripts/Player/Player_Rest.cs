using UnityEngine;
using System.Collections;
public class Player_Rest : EntityState
{
    float hold; 
    bool heal;

    public Player_Rest(Player player, StateMachine fsm, string animBool) : base(player, fsm, animBool) { }

    public void Setup(float fadeTime, float holdTime, bool healToFull)
    {
        hold = holdTime; 
        heal = healToFull;
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
        if (player.rb) player.rb.velocity = Vector2.zero;

        if (heal && player.stats != null)
            player.stats.HealToFull();
        player.StartCoroutine(player.BusyFor(hold + 0.2f)); 
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
    }

    public override void AnimationFinishTrigger()
    {
        stateMachine.ChangeState(player.idleState);
    }
}



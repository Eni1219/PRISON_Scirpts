using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;
    public EntityState(Player player,StateMachine stateMachine,string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb=player.rb;
        triggerCalled = false;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity",rb.velocity.y);
      
    }
    public virtual void Exit()
    {
        
        player.anim.SetBool(animBoolName, false);

    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

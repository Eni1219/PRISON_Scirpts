using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sinner : Enemy
{
    #region
    public SinnerIdle idleState {  get; private set; }
    public SinnerMove moveState { get; private set; }
    public SinnerAttack attackState { get; private set; }
    public SinnerBattle battleState { get; private set; }
    public SinnerStun stunState { get; private set; }
    public SinnerDead deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new SinnerIdle(this, stateMachine, "Idle", this);
        moveState = new SinnerMove(this, stateMachine, "Move", this);
        battleState = new SinnerBattle(this, stateMachine, "Move", this);
        attackState = new SinnerAttack(this, stateMachine, "Attack", this);
        stunState = new SinnerStun(this, stateMachine, "Stun", this);
        deadState = new SinnerDead(this, stateMachine, "Die", this);

    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void ResetToIdle()
    {
        base.ResetToIdle();
        stateMachine.ChangeState(idleState);
        lastTimeAttacked = -999f;   
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }
    public override void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Kasa : Enemy
{
    #region State
    
    public KasaIdle idleState { get; private set; }
    public KasaMove moveState { get; private set; }
    public KasaBattle battleState { get; private set; }
    public KasaAttack attackState { get; private set; }
    public KasaStun stunState { get; private set; }
    public KasaDie dieState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new KasaIdle(this, stateMachine, "Idle", this);
        moveState = new KasaMove(this, stateMachine, "Move", this);
        battleState = new KasaBattle(this, stateMachine, "Move", this);
        attackState = new KasaAttack(this, stateMachine, "Attack", this);
        stunState=new KasaStun(this, stateMachine,"Stun",this);
        dieState = new KasaDie(this, stateMachine, "Die", this);
        
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
    public override bool CanBeStunned()
    {
        if(base.CanBeStunned())
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
        stateMachine.ChangeState(dieState);
    }
    public override void ResetToIdle()
    {
        base.ResetToIdle();
        stateMachine.ChangeState(idleState);
        lastTimeAttacked = -999f;
    }
}

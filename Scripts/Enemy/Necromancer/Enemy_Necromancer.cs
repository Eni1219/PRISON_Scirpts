using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Necromancer :Enemy
{
    [Header("Specific Info")]
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float fireBallDamage;

    public Vector2 jumpVelocity;
    public float jumpCD;
    public float safeDistance;
    [HideInInspector]public float lastTimeJumped;
    #region
    public NecroIdle idleState {  get; private set; }
    public NecroMove moveState { get; private set; }
    public NecroAttack attackState { get; private set; }
    public NecroDead deadState { get; private set; }
    public NecroStun stunState { get; private set; }
    public NecroBattle battleState { get; private set; }
    public NecroJump jumpState { get; private set; }


    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new NecroIdle(this, stateMachine, "Idle", this);
        moveState = new NecroMove(this, stateMachine, "Move", this);
        attackState = new NecroAttack(this, stateMachine, "Attack", this);
        deadState = new NecroDead(this, stateMachine, "Dead", this);
        stunState = new NecroStun(this, stateMachine, "Stun", this);
        battleState = new NecroBattle(this, stateMachine, "Idle", this);
        jumpState = new NecroJump(this, stateMachine, "Jump", this);
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
   
    public override void ResetToIdle()
    {
        base.ResetToIdle();
        stateMachine.ChangeState(idleState);
        lastTimeAttacked = -999f;
    }
    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newFireBall = Instantiate(fireBall,attackCheck.position,Quaternion.identity);
        newFireBall.GetComponent<FireBall_Controller>().SetUpFireBall(fireBallSpeed * facingDir, stats);
    }
}

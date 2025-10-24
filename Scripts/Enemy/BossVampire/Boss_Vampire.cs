using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Boss_Vampire :Enemy
{
    private EnemyStats _enemyStats;
    public float safeDistance;
    [SerializeField] private Transform ProtectileCheck;
    [Header("Specific Info")]
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float fireBallDamage;
    [SerializeField] private GameObject flame;
    [SerializeField] private float flameSpeed;

    [Header("Atk3")]
    public float atk3LastUsedTime;
    public float atk3CD = 4f;

    public bool isPhase2 { get; private set; }
    public float phase2AttackSpeed = 1.1f;
    #region
    public Vampire_Ground groundState {  get; private set; }
    public Vampire_Idle idleState { get; private set; }
    public Vampire_Move moveState { get; private set; }
    public Vampire_Battle battleState { get; private set; }
    public Vampire_Attack1 attack1State { get; private set; }
    public Vampire_Attack2 attack2State { get; private set; }
    public Vampire_Attack3 attack3State { get; private set; }
    public Vampire_AttackCombo comboState { get; private set; }
    public Vampire_Dead deadState { get; private set; }


    #endregion

    protected override void Awake()
    {
        base.Awake();
        battleState = new Vampire_Battle(this, stateMachine, "Move", this);
        idleState = new Vampire_Idle(this, stateMachine, "Idle", this);
        moveState = new Vampire_Move(this, stateMachine, "Move", this);
        attack1State = new Vampire_Attack1(this, stateMachine, "Attack1", this);
        attack2State = new Vampire_Attack2(this, stateMachine, "Attack2", this);
        attack3State = new Vampire_Attack3(this, stateMachine, "Attack3", this);
        comboState = new Vampire_AttackCombo(this, stateMachine, "AttackCombo", this);
        deadState = new Vampire_Dead(this, stateMachine, "Die", this);
        _enemyStats=GetComponent<EnemyStats>();
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        if (_enemyStats.currentHealth <= _enemyStats.maxHealth * .4f)
        {
            EnterPhase2();
        }
        
    }

    public void EnterPhase2()
    {
        if (isPhase2) return;
        isPhase2 = true;
        Debug.Log("Boss Enters Phase2");
        if(anim)
        {
            anim.speed*=phase2AttackSpeed;
            moveSpeed = moveSpeed * 1.3f;
        }
    }
    public override void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
    }
    public override void Die()
    {
        base.Die();
        AudioManager.instance.Stop("BossFightBgm");
        BossGate bossGate=FindObjectOfType<BossGate>();
        if (bossGate != null)
        {
            bossGate.OpenGate();
        }
        stateMachine.ChangeState(deadState);
    }

    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newFireBall = Instantiate(fireBall, attackCheck.position, Quaternion.identity);
        newFireBall.GetComponent<FireBall_Controller>().SetUpFireBall(fireBallSpeed * facingDir, stats);
    }
    public override void BossProjectileAttackTrigger()
    {
        GameObject newFlame = Instantiate(flame, ProtectileCheck.position, Quaternion.identity);
        newFlame.GetComponent<Boss_ProjectileController>().SetUpBossProjectile(flameSpeed * facingDir, stats);
    }
}

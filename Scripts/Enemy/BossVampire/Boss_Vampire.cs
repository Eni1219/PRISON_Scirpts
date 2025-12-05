using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// ヴァンパイアボス敵のメインクラス。
/// 複数の攻撃パターンを持ち、HPが40%以下でフェーズ2に移行して強化されます。
/// </summary>
public class Boss_Vampire : Enemy
{
    /// <summary>ボスのEnemyStats参照</summary>
    private EnemyStats _enemyStats;

    /// <summary>プレイヤーから保つ安全距離</summary>
    public float safeDistance;

    /// <summary>投射物発射位置</summary>
    [SerializeField] private Transform ProtectileCheck;

    /// <summary>ファイアボールのプレハブ</summary>
    [Header("Specific Info")]
    [SerializeField] private GameObject fireBall;

    /// <summary>ファイアボールの移動速度</summary>
    [SerializeField] private float fireBallSpeed;

    /// <summary>ファイアボールのダメージ量</summary>
    [SerializeField] private float fireBallDamage;

    /// <summary>炎投射物のプレハブ</summary>
    [SerializeField] private GameObject flame;

    /// <summary>炎投射物の移動速度</summary>
    [SerializeField] private float flameSpeed;

    /// <summary>攻撃3の最後の使用時間</summary>
    [Header("Atk3")]
    public float atk3LastUsedTime;

    /// <summary>攻撃3のクールダウン時間（秒）</summary>
    public float atk3CD = 4f;

    /// <summary>フェーズ2に移行したかどうか</summary>
    public bool isPhase2 { get; private set; }

    /// <summary>フェーズ2の攻撃速度倍率</summary>
    public float phase2AttackSpeed = 1.1f;

    #region State
    /// <summary>地上状態</summary>
    public Vampire_Ground groundState { get; private set; }

    /// <summary>アイドル状態</summary>
    public Vampire_Idle idleState { get; private set; }

    /// <summary>移動状態</summary>
    public Vampire_Move moveState { get; private set; }

    /// <summary>戦闘状態</summary>
    public Vampire_Battle battleState { get; private set; }

    /// <summary>攻撃1状態</summary>
    public Vampire_Attack1 attack1State { get; private set; }

    /// <summary>攻撃2状態</summary>
    public Vampire_Attack2 attack2State { get; private set; }

    /// <summary>攻撃3状態</summary>
    public Vampire_Attack3 attack3State { get; private set; }

    /// <summary>コンボ攻撃状態</summary>
    public Vampire_AttackCombo comboState { get; private set; }

    /// <summary>死亡状態</summary>
    public Vampire_Dead deadState { get; private set; }
    #endregion

    /// <summary>
    /// Awake時の初期化処理。各状態を初期化します。
    /// </summary>
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
        _enemyStats = GetComponent<EnemyStats>();
    }

    /// <summary>
    /// Start時の初期化処理。初期状態をアイドルに設定します。
    /// </summary>
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    /// <summary>
    /// 毎フレームの更新処理。HPがしきい値以下でフェーズ2に移行します。
    /// </summary>
    protected override void Update()
    {
        base.Update();
        // HP40%以下でフェーズ2に移行
        if (_enemyStats.currentHealth <= _enemyStats.maxHealth * .4f)
        {
            EnterPhase2();
        }
    }

    /// <summary>
    /// フェーズ2に移行します。攻撃速度と移動速度が上昇します。
    /// </summary>
    public void EnterPhase2()
    {
        if (isPhase2) return;
        isPhase2 = true;
        Debug.Log("Boss Enters Phase2");
        if (anim)
        {
            // 攻撃速度と移動速度を上昇
            anim.speed *= phase2AttackSpeed;
            moveSpeed = moveSpeed * 1.3f;
        }
    }

    /// <summary>
    /// ダメージを受けた時のエフェクト処理。フラッシュのみ実行します。
    /// </summary>
    public override void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
    }

    /// <summary>
    /// 死亡時の処理。BGMを停止し、ボスゲートを開きます。
    /// </summary>
    public override void Die()
    {
        base.Die();
        AudioManager.instance.Stop("BossFightBgm");
        // ボスゲートを開放
        BossGate bossGate = FindObjectOfType<BossGate>();
        if (bossGate != null)
        {
            bossGate.OpenGate();
        }
        stateMachine.ChangeState(deadState);
    }

    /// <summary>
    /// 特殊攻撃（ファイアボール発射）トリガー。アニメーションから呼ばれます。
    /// </summary>
    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newFireBall = Instantiate(fireBall, attackCheck.position, Quaternion.identity);
        newFireBall.GetComponent<FireBall_Controller>().SetUpFireBall(fireBallSpeed * facingDir, stats);
    }

    /// <summary>
    /// ボス投射物攻撃（炎）トリガー。アニメーションから呼ばれます。
    /// </summary>
    public override void BossProjectileAttackTrigger()
    {
        GameObject newFlame = Instantiate(flame, ProtectileCheck.position, Quaternion.identity);
        newFlame.GetComponent<Boss_ProjectileController>().SetUpBossProjectile(flameSpeed * facingDir, stats);
    }
}

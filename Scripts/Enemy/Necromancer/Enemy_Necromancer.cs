using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ネクロマンサー敵のメインクラス。
/// 遠距離からファイアボールを発射する魔法使いタイプの敵です。
/// </summary>
public class Enemy_Necromancer : Enemy
{
    /// <summary>ファイアボールのプレハブ</summary>
    [Header("Specific Info")]
    [SerializeField] private GameObject fireBall;

    /// <summary>ファイアボールの移動速度</summary>
    [SerializeField] private float fireBallSpeed;

    /// <summary>ファイアボールのダメージ量</summary>
    [SerializeField] private float fireBallDamage;

    /// <summary>ジャンプ時の速度ベクトル</summary>
    public Vector2 jumpVelocity;

    /// <summary>ジャンプのクールダウン時間（秒）</summary>
    public float jumpCD;

    /// <summary>プレイヤーから保つ安全距離</summary>
    public float safeDistance;

    /// <summary>最後にジャンプした時間</summary>
    [HideInInspector] public float lastTimeJumped;

    #region State
    /// <summary>アイドル状態</summary>
    public NecroIdle idleState { get; private set; }

    /// <summary>移動状態</summary>
    public NecroMove moveState { get; private set; }

    /// <summary>攻撃状態</summary>
    public NecroAttack attackState { get; private set; }

    /// <summary>死亡状態</summary>
    public NecroDead deadState { get; private set; }

    /// <summary>スタン状態</summary>
    public NecroStun stunState { get; private set; }

    /// <summary>戦闘状態</summary>
    public NecroBattle battleState { get; private set; }

    /// <summary>ジャンプ状態</summary>
    public NecroJump jumpState { get; private set; }
    #endregion

    /// <summary>
    /// Awake時の初期化処理。各状態を初期化します。
    /// </summary>
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

    /// <summary>
    /// Start時の初期化処理。初期状態をアイドルに設定します。
    /// </summary>
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    /// <summary>
    /// 毎フレームの更新処理。
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// スタン可能かどうかをチェックし、可能ならスタン状態に遷移します。
    /// </summary>
    /// <returns>スタン可能な場合true</returns>
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ダメージを受けた時のエフェクト処理。フラッシュのみ実行します。
    /// </summary>
    public override void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
    }

    /// <summary>
    /// 死亡時の処理。死亡状態に遷移します。
    /// </summary>
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    /// <summary>
    /// アイドル状態にリセットします。
    /// </summary>
    public override void ResetToIdle()
    {
        base.ResetToIdle();
        stateMachine.ChangeState(idleState);
        lastTimeAttacked = -999f;
    }

    /// <summary>
    /// 特殊攻撃（ファイアボール発射）トリガー。アニメーションから呼ばれます。
    /// </summary>
    public override void AnimationSpecialAttackTrigger()
    {
        // ファイアボールを生成して発射
        GameObject newFireBall = Instantiate(fireBall, attackCheck.position, Quaternion.identity);
        newFireBall.GetComponent<FireBall_Controller>().SetUpFireBall(fireBallSpeed * facingDir, stats);
    }
}

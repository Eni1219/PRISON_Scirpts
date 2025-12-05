using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 笠（Kasa）敵のメインクラス。
/// 近接攻撃を行う一般的な敵タイプです。
/// </summary>
public class Enemy_Kasa : Enemy
{
    #region State
    /// <summary>アイドル状態</summary>
    public KasaIdle idleState { get; private set; }

    /// <summary>移動状態</summary>
    public KasaMove moveState { get; private set; }

    /// <summary>戦闘状態</summary>
    public KasaBattle battleState { get; private set; }

    /// <summary>攻撃状態</summary>
    public KasaAttack attackState { get; private set; }

    /// <summary>スタン状態</summary>
    public KasaStun stunState { get; private set; }

    /// <summary>死亡状態</summary>
    public KasaDie dieState { get; private set; }
    #endregion

    /// <summary>
    /// Awake時の初期化処理。各状態を初期化します。
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        idleState = new KasaIdle(this, stateMachine, "Idle", this);
        moveState = new KasaMove(this, stateMachine, "Move", this);
        battleState = new KasaBattle(this, stateMachine, "Move", this);
        attackState = new KasaAttack(this, stateMachine, "Attack", this);
        stunState = new KasaStun(this, stateMachine, "Stun", this);
        dieState = new KasaDie(this, stateMachine, "Die", this);
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
        stateMachine.ChangeState(dieState);
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
}

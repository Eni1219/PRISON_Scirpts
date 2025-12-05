using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// 全ての敵の基底クラス。
/// Entityを継承し、敵固有の機能（スタン、カウンター攻撃ウィンドウなど）を追加します。
/// </summary>
public class Enemy : Entity
{
    /// <summary>スタン状態の持続時間（秒）</summary>
    [Header("Stunned info")]
    public float stunDuration;

    /// <summary>スタン時のノックバック方向</summary>
    public Vector2 stunDir;

    /// <summary>現在スタン可能かどうか</summary>
    protected bool canBeStunned;

    /// <summary>カウンター可能な状態を示すUI画像</summary>
    [SerializeField] protected GameObject counterImage;

    /// <summary>プレイヤーを検出するレイヤーマスク</summary>
    [SerializeField] protected LayerMask PlayerLayerMask;

    /// <summary>移動速度</summary>
    [Header("Move info")]
    public float moveSpeed;

    /// <summary>アイドル状態の待機時間（秒）</summary>
    public float idleTime;

    /// <summary>戦闘状態の持続時間（秒）</summary>
    public float battleTime;

    /// <summary>攻撃を開始する距離</summary>
    [Header("Attack info")]
    public float attackDistance;

    /// <summary>攻撃のクールダウン時間（秒）</summary>
    public float attackCoolDown;

    /// <summary>最後に攻撃した時間</summary>
    [HideInInspector] public float lastTimeAttacked;

    /// <summary>死亡時に発火するイベント</summary>
    public event Action OnDeath;

    /// <summary>敵のステートマシン</summary>
    public EnemyStateMachine stateMachine { get; private set; }

    /// <summary>
    /// Awake時の初期化処理。ステートマシンを作成します。
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    /// <summary>
    /// 毎フレームの更新処理。現在の状態を更新します。
    /// </summary>
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    /// <summary>
    /// カウンター攻撃ウィンドウを開きます。スタン可能状態になります。
    /// </summary>
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    /// <summary>
    /// カウンター攻撃ウィンドウを閉じます。スタン不可状態になります。
    /// </summary>
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    /// <summary>
    /// スタン可能かどうかをチェックします。
    /// </summary>
    /// <returns>スタン可能な場合true</returns>
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    /// <summary>
    /// アニメーション完了トリガーを現在の状態に伝達します。
    /// </summary>
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    /// <summary>
    /// プレイヤーを検出するレイキャストを実行します。
    /// </summary>
    /// <returns>ヒットしたレイキャスト結果</returns>
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 20, PlayerLayerMask);

    /// <summary>
    /// エディタ上で攻撃範囲を可視化します。
    /// </summary>
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

    /// <summary>
    /// 敵をアイドル状態にリセットします。
    /// </summary>
    public virtual void ResetToIdle()
    {
        isKnocked = false;
        if (rb) rb.velocity = Vector2.zero;
        if (capsuleCollider) capsuleCollider.enabled = true;
    }

    /// <summary>
    /// 死亡時の処理。死亡イベントを発火します。
    /// </summary>
    public override void Die()
    {
        base.Die();
        OnDeath?.Invoke();
    }

    /// <summary>
    /// リスポーン処理。状態とコンポーネントをリセットします。
    /// </summary>
    public virtual void Respawn()
    {
        isKnocked = false;

        if (rb != null)
            rb.velocity = Vector2.zero;
        if (capsuleCollider != null)
            capsuleCollider.enabled = true;

        if (anim != null)
            anim.SetBool("Die", false);
    }

    /// <summary>
    /// 特殊攻撃アニメーショントリガー（サブクラスでオーバーライド）。
    /// </summary>
    public virtual void AnimationSpecialAttackTrigger()
    {

    }

    /// <summary>
    /// ボスの投射物攻撃トリガー（サブクラスでオーバーライド）。
    /// </summary>
    public virtual void BossProjectileAttackTrigger()
    {

    }
}

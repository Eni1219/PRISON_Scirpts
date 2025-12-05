using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーと敵の共通基底クラス（エンティティ）。
/// 移動、衝突判定、向き変更、ダメージ処理などの基本機能を提供します。
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>ノックバック方向ベクトル</summary>
    [Header("KnockBack Info")]
    [SerializeField] protected Vector2 knockbackDirection;

    /// <summary>ノックバック持続時間（秒）</summary>
    [SerializeField] protected float knockbackDuration;

    /// <summary>現在ノックバック中かどうか</summary>
    protected bool isKnocked;

    /// <summary>攻撃判定の中心位置</summary>
    [Header("Collision info")]
    public Transform attackCheck;

    /// <summary>攻撃判定の半径</summary>
    public float attackCheckRadius;

    /// <summary>地面判定の起点</summary>
    [SerializeField] protected Transform groundCheck;

    /// <summary>地面判定の距離</summary>
    [SerializeField] protected float groundCheckDistance;

    /// <summary>壁判定の起点</summary>
    [SerializeField] protected Transform wallCheck;

    /// <summary>壁判定の距離</summary>
    [SerializeField] protected float wallCheckDistance;

    /// <summary>地面として認識するレイヤー</summary>
    [SerializeField] protected LayerMask GroundLayer;

    /// <summary>現在の向き（1:右、-1:左）</summary>
    public int facingDir { get; private set; } = 1;

    /// <summary>右を向いているかどうか</summary>
    public bool facingRight = true;

    /// <summary>向き変更のクールダウン時間（秒）</summary>
    [SerializeField] private float flipCD = 0.1f;

    /// <summary>最後に向きを変えた時間</summary>
    private float lastFlipTime;

    #region Components
    /// <summary>アニメーターコンポーネント</summary>
    public Animator anim { get; private set; }

    /// <summary>エフェクト管理コンポーネント</summary>
    public EntityFX fx { get; private set; }

    /// <summary>物理演算用のRigidbody2D</summary>
    public Rigidbody2D rb { get; private set; }

    /// <summary>スプライトレンダラー</summary>
    public SpriteRenderer sr { get; private set; }

    /// <summary>キャラクターステータス</summary>
    public CharacterStats stats { get; private set; }

    /// <summary>カプセルコライダー</summary>
    public CapsuleCollider2D capsuleCollider { get; private set; }
    #endregion

    /// <summary>
    /// Awake時の初期化処理（オーバーライド可能）。
    /// </summary>
    protected virtual void Awake()
    {

    }

    /// <summary>
    /// Start時の初期化処理。各コンポーネントを取得します。
    /// </summary>
    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        stats = GetComponentInChildren<CharacterStats>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// 毎フレームの更新処理（オーバーライド可能）。
    /// </summary>
    protected virtual void Update()
    {

    }

    /// <summary>
    /// ダメージを受けた時のエフェクト処理。フラッシュとノックバックを実行します。
    /// </summary>
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    /// <summary>
    /// ヒット時のノックバック処理を行うコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        // 向きと反対方向にノックバック
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity
    /// <summary>
    /// 速度をゼロにします。ノックバック中は無効です。
    /// </summary>
    public void ZeroVelocity()
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(0, 0);
    }

    /// <summary>
    /// 速度を設定します。ノックバック中は無効です。
    /// </summary>
    /// <param name="_xVelocity">X方向の速度</param>
    /// <param name="_yVelocity">Y方向の速度</param>
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Collision
    /// <summary>
    /// 地面を検出しているかどうかを返します。
    /// </summary>
    /// <returns>地面を検出している場合true</returns>
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, GroundLayer);

    /// <summary>
    /// 壁を検出しているかどうかを返します。
    /// </summary>
    /// <returns>壁を検出している場合true</returns>
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, GroundLayer);

    /// <summary>
    /// エディタ上で衝突判定範囲を可視化します。
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    /// <summary>
    /// 向きを反転します。クールダウン中は無効です。
    /// </summary>
    public virtual void Flip()
    {
        // クールダウン中は反転しない
        if (Time.time - lastFlipTime < flipCD)
            return;
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        lastFlipTime = Time.time;
    }

    /// <summary>
    /// X方向の速度に応じて向きを制御します。
    /// </summary>
    /// <param name="_x">X方向の速度</param>
    public virtual void FlipController(float _x)
    {
        // 右に移動中で左を向いていたら反転
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        // 左に移動中で右を向いていたら反転
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    /// <summary>
    /// 死亡時の処理（オーバーライド可能）。
    /// </summary>
    public virtual void Die()
    {

    }
}

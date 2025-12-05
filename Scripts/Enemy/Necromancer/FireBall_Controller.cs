using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ファイアボール投射物を制御するクラス。
/// 一定速度で飛行し、プレイヤーや地面に当たるとダメージを与えて消滅します。
/// プレイヤーのカウンター攻撃で反射可能です。
/// </summary>
public class FireBall_Controller : MonoBehaviour
{
    /// <summary>与えるダメージ量</summary>
    [SerializeField] private int damage;

    /// <summary>ダメージを与える対象のレイヤー名</summary>
    [SerializeField] private string targetLayer = "Player";

    /// <summary>X方向の移動速度</summary>
    [SerializeField] private float xVelocity;

    /// <summary>飛行方向（1:右、-1:左）</summary>
    private int facingDir = 1;

    /// <summary>移動可能かどうか</summary>
    [SerializeField] private bool canMove;

    /// <summary>反射されたかどうか</summary>
    [SerializeField] private bool flipped;

    /// <summary>発射者のステータス（ダメージ計算用）</summary>
    private CharacterStats myStats;

    /// <summary>物理演算用のRigidbody2D</summary>
    [SerializeField] private Rigidbody2D rb;

    /// <summary>
    /// 毎フレームの更新処理。移動可能であれば速度を適用します。
    /// </summary>
    void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(xVelocity * facingDir, rb.velocity.y);
    }

    /// <summary>
    /// ファイアボールの初期設定を行います。
    /// </summary>
    /// <param name="_speed">移動速度（正:右、負:左）</param>
    /// <param name="_myStats">発射者のステータス</param>
    public void SetUpFireBall(float _speed, CharacterStats _myStats)
    {
        xVelocity = Mathf.Abs(_speed);
        myStats = _myStats;
        facingDir = _speed > 0 ? 1 : -1;
        // 右向きの場合はスプライトを反転
        if (facingDir == 1)
            transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// トリガーに接触した時の処理。対象にダメージを与えます。
    /// </summary>
    /// <param name="collision">接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 対象レイヤーに接触したらダメージを与えて停止
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            myStats.DoDamage(collision.GetComponent<CharacterStats>());
            StuckInto(collision);
        }
        // 地面に接触したら停止
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }

    /// <summary>
    /// 対象に刺さって停止する処理。
    /// </summary>
    /// <param name="collision">刺さる対象のコライダー</param>
    private void StuckInto(Collider2D collision)
    {
        // パーティクルを停止
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject);
    }

    /// <summary>
    /// ファイアボールを反射します。プレイヤーのカウンター攻撃で呼ばれます。
    /// </summary>
    public void FlipFireBall()
    {
        // 既に反射済みの場合は無視
        if (flipped)
            return;
        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        // 反射後は敵にダメージを与える
        targetLayer = "Enemy";
    }
}

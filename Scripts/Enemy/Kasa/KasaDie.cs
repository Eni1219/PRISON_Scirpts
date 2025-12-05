using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 笠敵の死亡状態クラス。
/// 死亡アニメーション再生後、オブジェクトを非アクティブ化します。
/// </summary>
public class KasaDie : EnemyState
{
    /// <summary>笠敵の参照</summary>
    Enemy_Kasa enemy;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public KasaDie(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    /// <summary>
    /// 状態開始時の処理。死亡アニメーションを開始し、コライダーを無効化します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(DestroyAfterAnimation());
        enemy.anim.SetBool("Die", true);
        // コライダーを無効化して衝突を防ぐ
        enemy.capsuleCollider.enabled = false;
    }

    /// <summary>
    /// 毎フレームの更新処理。速度をゼロに保ちます。
    /// </summary>
    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
    }

    /// <summary>
    /// 死亡アニメーション後にオブジェクトを非アクティブ化するコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1.6f);
        enemy.gameObject.SetActive(false);
    }
}

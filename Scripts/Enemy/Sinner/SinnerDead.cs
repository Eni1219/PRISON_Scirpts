using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シナー敵の死亡状態クラス。
/// 死亡アニメーション再生後、オブジェクトを非アクティブ化します。
/// </summary>
public class SinnerDead : EnemyState
{
    /// <summary>シナー敵の参照</summary>
    protected Enemy_Sinner enemy;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public SinnerDead(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Sinner enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    /// <summary>
    /// 状態開始時の処理。死亡アニメーションを開始します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(DestroyAfterAnimation());
        enemy.anim.SetBool("Die", true);
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
        yield return new WaitForSeconds(1f);
        enemy.gameObject.SetActive(false);
    }
}

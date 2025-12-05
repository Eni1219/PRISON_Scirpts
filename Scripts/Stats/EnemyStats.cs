using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵専用のステータス管理クラス。
/// CharacterStatsを継承し、敵固有の処理（リスポーン、死亡アニメーションなど）を追加します。
/// </summary>
public class EnemyStats : CharacterStats
{
    /// <summary>敵コンポーネントの参照</summary>
    private Enemy enemy;

    /// <summary>Rigidbody2Dの参照</summary>
    private Rigidbody2D rb;

    /// <summary>
    /// 初期化処理。敵コンポーネントとRigidbodyを取得します。
    /// </summary>
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ダメージを受けた時の処理。ダメージエフェクトを再生します。
    /// </summary>
    /// <param name="_damage">受けるダメージ量</param>
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.DamageEffect();
    }

    /// <summary>
    /// 死亡時の処理。敵の死亡処理を呼び出します。
    /// </summary>
    public override void Die()
    {
        base.Die();
        enemy?.Die();
    }

    /// <summary>
    /// 死亡アニメーション完了時に呼ばれます。オブジェクトを非アクティブ化します。
    /// </summary>
    public void OnDeathAnimationFinished()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// リスポーン用のリセット処理。HPを全回復し、状態を初期化します。
    /// </summary>
    public void RestForRespawn()
    {
        gameObject.SetActive(true);
        // 速度をリセット
        if (rb)
            rb.velocity = Vector2.zero;
        // HPを全回復
        HealToFull();
        // 敵の状態をリセット
        if (enemy)
        {
            enemy.ResetToIdle();
        }
    }
}

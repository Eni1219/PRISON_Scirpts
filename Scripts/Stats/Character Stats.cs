using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// キャラクター（プレイヤー・敵共通）のステータスを管理する基底クラス。
/// HP、攻撃力、ダメージ処理、回復処理などを提供します。
/// IHealthインターフェースを実装してUI連携を可能にします。
/// </summary>
public class CharacterStats : MonoBehaviour, IHealth
{
    /// <summary>筋力ステータス（ダメージ計算に使用）</summary>
    public Stats strength;

    /// <summary>最大HPステータス</summary>
    public Stats maxHP;

    /// <summary>攻撃力ステータス</summary>
    public Stats damage;

    /// <summary>現在のHP</summary>
    [SerializeField] private int currentHP;

    /// <summary>最大HP値（計算後）</summary>
    public int maxHealth => maxHP.GetValue();

    /// <summary>現在HP値（読み取り専用）</summary>
    public int currentHealth => currentHP;

    /// <summary>HP変更時に発火するイベント（現在HP, 最大HP）</summary>
    public event Action<int, int> OnHealthChanged;

    /// <summary>死亡時に発火するイベント</summary>
    public event Action OnDied;

    /// <summary>
    /// 初期化処理。HPを最大値に設定します。
    /// </summary>
    protected virtual void Start()
    {
        currentHP = maxHealth;
        OnHealthChanged?.Invoke(currentHP, maxHealth);
    }

    /// <summary>
    /// 対象にダメージを与えます。
    /// </summary>
    /// <param name="_targetStats">ダメージを与える対象のステータス</param>
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        // 攻撃力と筋力を合計してダメージを計算
        int totalDamage = damage.GetValue() + strength.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    /// <summary>
    /// ダメージを受けます。HPが0以下になると死亡処理を実行します。
    /// </summary>
    /// <param name="_damage">受けるダメージ量</param>
    public virtual void TakeDamage(int _damage)
    {
        if (_damage <= 0) return;

        currentHP -= _damage;

        // HPが0以下になったら死亡
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnHealthChanged?.Invoke(currentHP, maxHealth);
            Die();
            OnDied?.Invoke();
        }
        else
            OnHealthChanged?.Invoke(currentHP, maxHealth);
    }

    /// <summary>
    /// HPを回復します。最大HPを超えません。
    /// </summary>
    /// <param name="amount">回復量</param>
    public virtual void Heal(int amount)
    {
        if (amount <= 0) return;
        // 最大HPを超えないように制限
        currentHP = Mathf.Min(currentHP + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHP, maxHealth);
    }

    /// <summary>
    /// HPを最大まで回復します。
    /// </summary>
    public void HealToFull()
    {
        currentHP = maxHealth;
        OnHealthChanged?.Invoke(currentHP, maxHealth);
    }

    /// <summary>
    /// 死亡時の処理。サブクラスでオーバーライドして使用します。
    /// </summary>
    public virtual void Die()
    {

    }
}

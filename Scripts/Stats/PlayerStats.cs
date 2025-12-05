using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー専用のステータス管理クラス。
/// CharacterStatsを継承し、プレイヤー固有の処理（攻撃力ブースト、HP最大値アップなど）を追加します。
/// </summary>
public class PlayerStats : CharacterStats
{
    /// <summary>プレイヤーコンポーネントの参照</summary>
    private Player player;

    /// <summary>
    /// 初期化処理。プレイヤーコンポーネントを取得します。
    /// </summary>
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    /// <summary>
    /// ダメージを受けた時の処理。効果音とエフェクトを再生します。
    /// </summary>
    /// <param name="_damage">受けるダメージ量</param>
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        AudioManager.instance.Play("PlayerHurt");
        player.DamageEffect();
    }

    /// <summary>
    /// 死亡時の処理。プレイヤーの死亡処理を呼び出します。
    /// </summary>
    public override void Die()
    {
        base.Die();
        player.Die();
    }

    /// <summary>
    /// 攻撃力を永続的に増加させます。
    /// </summary>
    /// <param name="amount">増加量</param>
    public void BoostAttack(int amount)
    {
        if (amount > 0)
        {
            damage.AddModifier(amount);
            Debug.Log($"AtkUP{amount}! new Atk：{damage.GetValue()}");
        }
    }

    /// <summary>
    /// 最大HPを永続的に増加させ、増加分だけ現在HPも回復します。
    /// </summary>
    /// <param name="amount">増加量</param>
    public void BoostMaxHealth(int amount)
    {
        if (amount > 0)
        {
            int oldMaxHealth = maxHealth;
            maxHP.AddModifier(amount);
            // 増加分だけ現在HPも回復
            int healthIncrease = maxHealth - oldMaxHealth;
            Heal(healthIncrease);
        }
    }

    /// <summary>
    /// HPを回復し、回復エフェクトを再生します。
    /// </summary>
    /// <param name="amount">回復量</param>
    public override void Heal(int amount)
    {
        base.Heal(amount);
        player.HealEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        AudioManager.instance.Play("PlayerHurt");
        player.DamageEffect();
    }
    public override void Die()
    {
        base.Die();
        player.Die();
    }
    public void BoostAttack(int amount)
    {
        if(amount>0)
        {
            damage.AddModifier(amount);
            Debug.Log($"AtkUP{amount}! new Atk£º{damage.GetValue()}");
        }
    }
    public void BoostMaxHealth(int amount)
    {
        if(amount>0)
        {
            int oldMaxHealth = maxHealth;
            maxHP.AddModifier(amount);
            int healthIncrease=maxHealth-oldMaxHealth;
            Heal(healthIncrease);
        }
    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
        player.HealEffect();
    }
}

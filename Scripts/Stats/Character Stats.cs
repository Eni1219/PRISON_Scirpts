using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CharacterStats : MonoBehaviour,IHealth
{
    public Stats strength;
    public Stats  maxHP;
    public Stats damage;
    [SerializeField] private int currentHP;
    public int maxHealth => maxHP.GetValue();
    public int currentHealth => currentHP;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDied;

    protected virtual void Start()
    {
        currentHP=maxHealth;
        OnHealthChanged?.Invoke(currentHP,maxHealth);
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage=damage.GetValue()+strength.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void TakeDamage(int _damage)
    {
        if(_damage<=0) return;

        currentHP-=_damage;
      
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
    public virtual void Heal(int amount)
    {
        if (amount <= 0) return;
        currentHP = Mathf.Min(currentHP + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHP, maxHealth);
    }
    public void HealToFull()
    {
        currentHP = maxHealth;
        OnHealthChanged?.Invoke(currentHP, maxHealth);
    }
    public virtual void Die()
    {

    }
}

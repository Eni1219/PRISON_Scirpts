using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private Rigidbody2D rb;


   
    protected override  void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        rb=GetComponent<Rigidbody2D>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.DamageEffect();
    }
    public override void Die()
    {
        base.Die();
        enemy?.Die();   
    }
    public void OnDeathAnimationFinished()
    {
        gameObject.SetActive(false);
    }
    public void RestForRespawn()
    {
        gameObject.SetActive(true);
        if (rb)
            rb.velocity = Vector2.zero;
        HealToFull();
        if (enemy)
        {
            enemy.ResetToIdle();
           
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Dead : EnemyState
{
    protected Boss_Vampire enemy;
    public Vampire_Dead(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Vampire _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(DestroyAfterAnimation());
        enemy.anim.SetBool("Die", true);
        enemy.capsuleCollider.enabled = false;

    }



    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1f);
        Object.Destroy(enemy.gameObject);
    }
}

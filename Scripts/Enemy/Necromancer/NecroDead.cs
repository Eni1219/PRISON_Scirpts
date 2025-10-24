using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroDead : EnemyState
{
    protected Enemy_Necromancer enemy;
    public NecroDead(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necromancer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
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
        yield return new WaitForSeconds(1.6f);
        enemy.gameObject.SetActive(false);
    }
}

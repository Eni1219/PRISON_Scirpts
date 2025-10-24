using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinnerDead : EnemyState
{
    protected Enemy_Sinner enemy;
    public SinnerDead(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Sinner enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(DestroyAfterAnimation());
        enemy.anim.SetBool("Die", true);
    

    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1f);
        enemy.gameObject.SetActive(false);
    }
}

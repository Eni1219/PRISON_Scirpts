using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasaDie : EnemyState
{
    Enemy_Kasa enemy;
    public KasaDie(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        yield return new WaitForSeconds(1.6f);
        enemy.gameObject.SetActive(false);
    }
}

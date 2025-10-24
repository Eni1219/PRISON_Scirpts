using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasaAttack : EnemyState
{
    private Enemy_Kasa enemy;
    public KasaAttack(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();
        
        if(triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}

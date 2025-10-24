using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinnerAttack : EnemyState
{
    protected Enemy_Sinner enemy;
    public SinnerAttack(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Sinner _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;    
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

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
        enemy.ZeroVelocity();
    }
}

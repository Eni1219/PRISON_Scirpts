using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroAttack :EnemyState
{
    protected Enemy_Necromancer enemy;
    public NecroAttack(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necromancer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}

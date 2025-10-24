using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroIdle : NecroGround
{
    protected Enemy_Necromancer _enemy;
    public NecroIdle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName,_enemy)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0f)
            stateMachine.ChangeState(enemy.moveState);

    }
}

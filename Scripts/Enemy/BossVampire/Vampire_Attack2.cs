using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Attack2 : EnemyState
{
    protected Boss_Vampire enemy;
    public float atk2CD = 8f;
    private float atk2LastUsedTime = -999f;
    public Vampire_Attack2(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Vampire _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        atk2LastUsedTime=Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);

    }
    public bool IsOffCoolDown2() => Time.time > atk2LastUsedTime + atk2CD;
}

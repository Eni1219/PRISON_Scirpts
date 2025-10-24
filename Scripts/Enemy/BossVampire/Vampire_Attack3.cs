using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Attack3 : EnemyState
{
    protected Boss_Vampire enemy;
    public float atk3CD = 4f;
    private float atk3LastUsedTime = -999f;

    public Vampire_Attack3(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Vampire _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemy.ZeroVelocity();
        atk3LastUsedTime=Time.time;
    
    }

    public override void Update()
    {
        base.Update();
        
        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);   
        }
    }
    public bool IsOffCoolDown3()=>Time.time > atk3LastUsedTime+atk3CD;
}

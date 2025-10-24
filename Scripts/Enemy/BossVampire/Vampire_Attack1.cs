using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Attack1 : EnemyState
{
    protected Boss_Vampire enemy;
    public float atk1CD=2f;
    private float atk1LastUsedTime=-999f;
    public Vampire_Attack1(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Vampire _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemy.ZeroVelocity();
        

        if (triggerCalled)
        { 
            atk1LastUsedTime = Time.time;
            stateMachine.ChangeState(enemy.battleState);
        
        }
    }
    public bool IsOffCoolDown1()
    {

        return Time.time > atk1LastUsedTime + atk1CD;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_AttackCombo : EnemyState
{
    protected Boss_Vampire enemy;
    public float atkComboCD = 2f;
    private float atkComboLastUsedTime = -999f;

    public Vampire_AttackCombo(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Boss_Vampire _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
            atkComboLastUsedTime = Time.time;
            stateMachine.ChangeState(enemy.battleState);

        }
    }
    public bool IsOffCoolDownCombo()
    {

        return Time.time > atkComboLastUsedTime + atkComboCD;

    }


}

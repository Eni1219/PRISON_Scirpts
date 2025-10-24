using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Ground : EnemyState
{
    protected Boss_Vampire enemy;
    protected Transform player;
    public Vampire_Ground(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Boss_Vampire _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < enemy.attackDistance)
            stateMachine.ChangeState(enemy.battleState);
    }
}

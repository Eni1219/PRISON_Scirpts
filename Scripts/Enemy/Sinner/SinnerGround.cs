using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinnerGround : EnemyState
{
    protected Enemy_Sinner enemy;
    protected Transform player;
    public SinnerGround(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Sinner enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy=enemy;
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
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinnerMove : SinnerGround
{
    private Enemy_Sinner enemy;
    public SinnerMove(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Sinner _enemy) : base(_enemyBase, _stateMachine, _animBoolName,_enemy)
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
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
        if (enemy.isWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }

    }
}

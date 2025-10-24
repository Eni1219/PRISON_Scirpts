using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasaMove :KasaGround
{
    public KasaMove(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa enemy) : base(_enemyBase, _stateMachine, _animBoolName,enemy)
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
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed*enemy.facingDir,rb.velocity.y);
        if(enemy.isWallDetected()||!enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
      
    }
}

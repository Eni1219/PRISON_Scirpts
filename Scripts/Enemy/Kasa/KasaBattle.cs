using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasaBattle : EnemyState
{
    private Transform player;
    private Enemy_Kasa enemy;
    private int moveDir;
    public KasaBattle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Kasa enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy= enemy;
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
    private bool CanAttack()
    {
        if(Time.time>=enemy.lastTimeAttacked+enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked= Time.time;
            return true;
        }
        Debug.Log("Attack CD");
        return false;
    }
    public override void Update()
    {
        base.Update();
        if(enemy.IsPlayerDetected())
        {
            stateTimer=enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
         
        }
        else
        {
            if(stateTimer<0||Vector2.Distance(player.transform.position,enemy.transform.position)>7)
                stateMachine.ChangeState(enemy.idleState);
        }
            
            
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if(player.position.x < enemy.transform.position.x)
            moveDir=-1;
        enemy.SetVelocity(moveDir * enemy.moveSpeed,  rb.velocity.y);
    }
}

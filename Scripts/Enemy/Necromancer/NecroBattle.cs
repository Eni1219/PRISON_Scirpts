using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroBattle : EnemyState
{
    protected Transform player;
    protected Enemy_Necromancer enemy;
    protected int moveDir;
    public NecroBattle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.safeDistance)
            {
                if (CanJump())
                    stateMachine.ChangeState(enemy.jumpState);
            }
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }

        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                stateMachine.ChangeState(enemy.idleState);
        }

    }
    private bool CanJump()
    {
        if(Time.time>=enemy.lastTimeJumped+enemy.jumpCD)
        {
            enemy.lastTimeJumped = Time.time;
            return true;
        }
        return false;
    }
}

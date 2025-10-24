using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Battle : EnemyState
{
    protected Boss_Vampire enemy;
    protected Transform player;
    protected int moveDir;
    protected Player _player;
    bool atk3BurstActive;
    int atk3ShotLeft;
    float atk3Interval = .35f;
    float atk3IntervalTimer;

   
    public Vampire_Battle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_Vampire enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
        if(player)
        {
            _player = player.GetComponent<Player>();
        }
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

        var hit = enemy.IsPlayerDetected();
        bool detected = hit.collider != null;
        float dist = detected ? hit.distance:float.MaxValue;
         
        if (detected)
        {
            stateTimer = enemy.battleTime;
            if (dist< enemy.attackDistance&&enemy.attack1State.IsOffCoolDown1())
            {
                stateMachine.ChangeState(enemy.attack1State);   
                return;

            }
            if(dist<enemy.safeDistance&&enemy.attack2State.IsOffCoolDown2()&&enemy.isPhase2)
            {
                stateMachine.ChangeState(enemy.attack2State);
                return;
            }
            if(dist>enemy.attackDistance&&dist<=enemy.safeDistance&&enemy.attack3State.IsOffCoolDown3())
            {
                stateMachine.ChangeState(enemy.attack3State);
                return; 
            }
            if(dist<enemy.attackDistance&&enemy.comboState.IsOffCoolDownCombo()&&enemy.isPhase2)
            {
                stateMachine.ChangeState(enemy.comboState);
                return;
            }

        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
                stateMachine.ChangeState(enemy.idleState);
            else if (_player!=null&&_player.isDead)
            {
                stateMachine.ChangeState(enemy.moveState);
                return ;
            }
        }


        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;
        enemy.SetVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);
    }
}

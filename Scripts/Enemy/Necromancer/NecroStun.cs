using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroStun : EnemyState
{
    protected Enemy_Necromancer enemy;
    public NecroStun(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necromancer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);
        stateTimer = enemy.stunDuration;
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDir.x, enemy.stunDir.y);
        ;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}

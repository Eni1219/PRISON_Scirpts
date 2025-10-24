using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroJump : EnemyState
{
    protected Enemy_Necromancer enemy;
    public NecroJump(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necromancer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity=new Vector2(enemy.jumpVelocity.x*-enemy.facingDir,enemy.jumpVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.anim.SetFloat("Jump", rb.velocity.y);

        if (rb.velocity.y < 0 && enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.battleState);
    }
}

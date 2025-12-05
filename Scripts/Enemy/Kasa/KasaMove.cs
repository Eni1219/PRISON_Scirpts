using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 笠敵の移動状態クラス。
/// 壁や端を検出したら向きを変えてアイドル状態に戻ります。
/// </summary>
public class KasaMove : KasaGround
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public KasaMove(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
        this.enemy = enemy;
    }

    /// <summary>
    /// 状態開始時の処理。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理。前進し、壁や端で反転します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 現在の向きに移動
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
        // 壁検出または地面がない場合は反転してアイドル状態へ
        if (enemy.isWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シナー敵のアイドル状態クラス。
/// 一定時間待機後、移動状態に遷移します。
/// </summary>
public class SinnerIdle : SinnerGround
{
    /// <summary>シナー敵の参照</summary>
    protected Enemy_Sinner enemy;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public SinnerIdle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Sinner _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
        this.enemy = _enemy;
    }

    /// <summary>
    /// 状態開始時の処理。待機タイマーを設定します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理。タイマー終了で移動状態へ。
    /// </summary>
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0f)
            stateMachine.ChangeState(enemy.moveState);
    }
}

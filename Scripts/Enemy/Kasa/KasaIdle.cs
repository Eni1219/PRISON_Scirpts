using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 笠敵のアイドル状態クラス。
/// 一定時間待機後、移動状態に遷移します。
/// </summary>
public class KasaIdle : KasaGround
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public KasaIdle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
        this.enemy = enemy;
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

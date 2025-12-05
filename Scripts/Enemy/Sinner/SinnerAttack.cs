using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シナー敵の攻撃状態クラス。
/// 攻撃アニメーション中は停止し、完了後戦闘状態に戻ります。
/// </summary>
public class SinnerAttack : EnemyState
{
    /// <summary>シナー敵の参照</summary>
    protected Enemy_Sinner enemy;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public SinnerAttack(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Sinner _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
    /// 毎フレームの更新処理。攻撃中は停止し、完了で戦闘状態へ。
    /// </summary>
    public override void Update()
    {
        base.Update();

        // アニメーション完了で戦闘状態に戻る
        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
        // 攻撃中は動かない
        enemy.ZeroVelocity();
    }
}

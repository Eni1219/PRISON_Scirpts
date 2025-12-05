using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアイドル（待機）状態クラス。
/// 移動入力があれば移動状態に遷移します。
/// </summary>
public class Player_Idle : Player_Ground
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Idle(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    /// <summary>
    /// 状態開始時の処理。着地処理と速度リセットを行います。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        player.Landed();
        player.ZeroVelocity();
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理。水平入力があれば移動状態へ遷移します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 水平入力があり、ビジー状態でなければ移動状態へ
        if (xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}

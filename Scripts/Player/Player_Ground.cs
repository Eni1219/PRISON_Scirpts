using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの地上状態の基底クラス。
/// 地上でのジャンプ、攻撃、カウンターなどの入力を処理します。
/// </summary>
public class Player_Ground : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Ground(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
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
    /// 毎フレームの更新処理。地上での各種入力を監視します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // カウンター攻撃（Kキー、スキル解放済みの場合）
        if (Input.GetKeyDown(KeyCode.K) && player.canCounter)
            stateMachine.ChangeState(player.counterAttack);
        // 通常攻撃（Jキー）
        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.primaryAttack);
        // 地面から落ちた場合は空中状態へ
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
        // ジャンプ（スペースキー、地面にいる場合）
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
        // 回復（Qキー）
        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.healState);
    }
}

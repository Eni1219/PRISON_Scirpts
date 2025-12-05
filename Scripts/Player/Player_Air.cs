using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの空中状態クラス。
/// 空中移動、二段ジャンプ、壁スライド、空中攻撃などの入力を処理します。
/// </summary>
public class Player_Air : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Air(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
    /// 毎フレームの更新処理。空中での各種入力と状態遷移を処理します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 着地したらアイドル状態へ
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        // 壁に接触したら壁スライド状態へ（スキル解放済みの場合）
        if (player.isWallDetected())
        {
            if (player.canWallSlide)
                stateMachine.ChangeState(player.wallSlideState);
        }
        // 空中攻撃（Jキー）
        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.airCombo);
        // 二段ジャンプ（スペースキー、スキル解放済みかつ未使用の場合）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.canDoubleJump && !player.doubleJumpUsed)
            {
                player.doubleJumpUsed = true;
                stateMachine.ChangeState(player.jumpState);
                AudioManager.instance.Play("Jump");
            }
        }
        // 空中での水平移動（地上より若干遅い）
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}

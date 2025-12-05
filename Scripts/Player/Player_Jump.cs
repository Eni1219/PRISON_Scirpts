using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ状態クラス。
/// ジャンプ力を適用し、下降開始で空中状態へ遷移します。
/// </summary>
public class Player_Jump : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Jump(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。上向きの力を加えてジャンプSEを再生します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // ジャンプ力を適用
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        AudioManager.instance.Play("Jump");
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理。下降開始で空中状態へ遷移します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 下降開始（y速度が負）で空中状態へ
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの壁スライド状態クラス。
/// 壁に張り付いてゆっくり落下し、壁ジャンプが可能です。
/// </summary>
public class Player_WallSlide : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_WallSlide(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。スプライトを反転し、重力を軽減します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // 壁側を向くためにスプライトを反転
        player.sr.flipX = true;
        // 落下速度を遅くするため重力を軽減
        rb.gravityScale = 0.5f;
    }

    /// <summary>
    /// 状態終了時の処理。スプライトと重力を元に戻します。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        player.sr.flipX = false;
        rb.gravityScale = 3f;
    }

    /// <summary>
    /// 毎フレームの更新処理。壁ジャンプや離脱の入力を処理します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // スペースキーで壁ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        // 壁から離れる方向に入力したらアイドル状態へ
        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);
        // 下入力で通常落下、それ以外はゆっくり落下
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .8f);
        // 地面に着いたらアイドル状態へ
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
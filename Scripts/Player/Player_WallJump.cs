using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの壁ジャンプ状態クラス。
/// 壁から離れる方向にジャンプし、一定時間後に空中状態へ遷移します。
/// </summary>
public class Player_WallJump : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_WallJump(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。壁から離れる方向にジャンプ力を適用します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        // 壁と反対方向に水平速度、上向きにジャンプ力を設定
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
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
    /// 毎フレームの更新処理。タイマー終了で空中状態へ遷移します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 壁ジャンプの制御時間終了で空中状態へ
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
    }
}

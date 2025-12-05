using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのダッシュ状態クラス。
/// 一定時間高速移動し、終了後アイドル状態へ遷移します。
/// </summary>
public class Player_Dash : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Dash(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。ダッシュSEを再生し、継続時間を設定します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.Play("Dash");
        stateTimer = player.dashDuration;
    }

    /// <summary>
    /// 状態終了時の処理。水平速度をリセットします。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    /// <summary>
    /// 毎フレームの更新処理。ダッシュ速度を適用し、時間経過でアイドル状態へ。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // ダッシュ方向と速度を設定（垂直速度は0に固定）
        player.SetVelocity(player.dashDir * player.dashSpeed, 0);

        // ダッシュ時間終了でアイドル状態へ
        if ((stateTimer < 0))
            stateMachine.ChangeState(player.idleState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動状態クラス。
/// 足音SEを再生し、入力に応じて移動します。
/// </summary>
public class Player_Move : Player_Ground
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Move(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。足音SEのループ再生を開始します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // 足音SEの設定と再生
        if (player.footStepSE && player.audioSource)
        {
            player.audioSource.clip = player.footStepSE;
            player.audioSource.loop = true;
            player.audioSource.Play();
        }
    }

    /// <summary>
    /// 状態終了時の処理。足音SEを停止します。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        if (player.audioSource && player.audioSource.isPlaying)
            player.audioSource.Stop();
    }

    /// <summary>
    /// 毎フレームの更新処理。入力に応じて移動し、入力がなければアイドル状態へ。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 入力方向に移動
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        // 入力がなければアイドル状態へ遷移
        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの死亡状態クラス。
/// 死亡アニメーションを再生し、ゲームオーバーUIを表示します。
/// </summary>
public class Player_Death : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Death(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// アニメーション完了トリガー。
    /// </summary>
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    /// <summary>
    /// 状態開始時の処理。速度をゼロにし、死亡レイヤーに変更、UI表示を行います。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        // 死亡レイヤーに変更（他オブジェクトとの衝突を無効化）
        player.gameObject.layer = LayerMask.NameToLayer("Dead");
        AudioManager.instance.Play("PlayerDeath");
        UIManager.instance.ShowDeathUI();
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理（死亡中は何もしない）。
    /// </summary>
    public override void Update()
    {
        base.Update();
    }
}

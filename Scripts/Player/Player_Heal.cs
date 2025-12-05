using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの回復状態クラス。
/// 回復アニメーションを再生し、完了後アイドル状態へ戻ります。
/// </summary>
public class Player_Heal : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_Heal(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。回復SEを再生します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.Play("Heal");
    }

    /// <summary>
    /// 毎フレームの更新処理。回復中は移動できません。
    /// </summary>
    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        // アニメーション完了でアイドル状態へ
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        triggerCalled = true;
    }
}

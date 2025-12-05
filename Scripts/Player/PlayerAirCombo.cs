using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの空中コンボ攻撃状態クラス。
/// 空中で最大2段のコンボ攻撃を行います。
/// </summary>
public class PlayerAirCombo : EntityState
{
    /// <summary>現在の空中コンボ段数</summary>
    private int airComboCounter;

    /// <summary>最後に攻撃した時間</summary>
    private float lastTimeAttacked;

    /// <summary>コンボ入力受付時間（秒）</summary>
    private float comboWindow = 2;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public PlayerAirCombo(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。コンボカウンターを設定し、攻撃を実行します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // コンボがリセットされる条件をチェック（2段目以降またはコンボ時間切れ）
        if (airComboCounter > 1 || Time.time >= lastTimeAttacked + comboWindow)
            airComboCounter = 0;
        stateTimer = .1f;
        player.anim.SetInteger("AirCombo", airComboCounter);
        player.anim.speed = 1.2f;

        #region Attack Direction
        // 攻撃方向の決定
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;
        #endregion
        // 攻撃時の移動量を適用
        player.SetVelocity(player.attackMovement[airComboCounter].x * attackDir, player.attackMovement[airComboCounter].y);
    }

    /// <summary>
    /// 状態終了時の処理。コンボカウンターを増加させます。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .2f);
        player.anim.speed = 1;
        airComboCounter++;
        lastTimeAttacked = Time.time;
    }

    /// <summary>
    /// 毎フレームの更新処理。アニメーション完了で空中状態へ遷移します。
    /// </summary>
    public override void Update()
    {
        base.Update();

        // タイマー終了後は速度をゼロに
        if (stateTimer < 0)
        {
            player.ZeroVelocity();
        }

        // アニメーション完了で空中状態へ
        if (triggerCalled)
            stateMachine.ChangeState(player.airState);
    }
}

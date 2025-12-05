using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの休憩状態クラス。
/// 休憩ポイントでの回復処理とアニメーションを管理します。
/// </summary>
public class Player_Rest : EntityState
{
    /// <summary>休憩の保持時間</summary>
    float hold;

    /// <summary>HPを全回復するかどうか</summary>
    bool heal;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="fsm">ステートマシン</param>
    /// <param name="animBool">アニメーションパラメータ名</param>
    public Player_Rest(Player player, StateMachine fsm, string animBool) : base(player, fsm, animBool) { }

    /// <summary>
    /// 休憩状態の設定を行います。StartRestから呼び出されます。
    /// </summary>
    /// <param name="fadeTime">フェード時間</param>
    /// <param name="holdTime">休憩保持時間</param>
    /// <param name="healToFull">HPを全回復するかどうか</param>
    public void Setup(float fadeTime, float holdTime, bool healToFull)
    {
        hold = holdTime;
        heal = healToFull;
    }

    /// <summary>
    /// 状態開始時の処理。速度をゼロにし、設定に応じてHPを回復します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
        if (player.rb) player.rb.velocity = Vector2.zero;

        // 設定に応じてHPを全回復
        if (heal && player.stats != null)
            player.stats.HealToFull();
        // 休憩中はビジー状態に
        player.StartCoroutine(player.BusyFor(hold + 0.2f));
    }

    /// <summary>
    /// 毎フレームの更新処理。休憩中は動かないようにします。
    /// </summary>
    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
    }

    /// <summary>
    /// アニメーション完了トリガー。アイドル状態へ遷移します。
    /// </summary>
    public override void AnimationFinishTrigger()
    {
        stateMachine.ChangeState(player.idleState);
    }
}



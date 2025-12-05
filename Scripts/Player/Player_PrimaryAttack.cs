using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの通常攻撃状態クラス。
/// 最大3段のコンボ攻撃を行い、アニメーション完了でアイドル状態へ戻ります。
/// </summary>
public class Player_PrimaryAttack : EntityState
{
    /// <summary>現在のコンボ段数</summary>
    private int comboCounter;

    /// <summary>最後に攻撃した時間</summary>
    private float lastTimeAttacked;

    /// <summary>コンボ入力受付時間（秒）</summary>
    private float comboWindow = 2;

    /// <summary>入力バッファの時間（秒）</summary>
    private float inputBufferTimer = .1f;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public Player_PrimaryAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。コンボカウンターを設定し、攻撃方向を決定します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // コンボがリセットされる条件をチェック（3段目以降またはコンボ時間切れ）
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        stateTimer = .1f;
        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1.2f;
        player.StartCoroutine(DetermineAttackDir());
    }

    /// <summary>
    /// 攻撃方向を決定し、攻撃時の移動を適用するコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator DetermineAttackDir()
    {
        yield return null;

        #region Attack Direction
        // 入力方向または現在の向きで攻撃方向を決定
        float attackDir;
        if (xInput != 0)
        {
            attackDir = Mathf.Sign(xInput);
            // 入力方向と向きが違う場合は反転
            if (xInput != player.facingDir)
                player.Flip();
        }
        else
        {
            attackDir = player.facingDir;
        }
        #endregion
        // 攻撃時の移動量を適用
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
    }

    /// <summary>
    /// 状態終了時の処理。コンボカウンターを増加させ、攻撃時間を記録します。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .2f);
        player.anim.speed = 1;
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    /// <summary>
    /// 毎フレームの更新処理。攻撃アニメーション完了でアイドル状態へ遷移します。
    /// </summary>
    public override void Update()
    {
        base.Update();

        // タイマー終了後は速度をゼロに
        if (stateTimer < 0)
            player.ZeroVelocity();
        // アニメーション完了でアイドル状態へ
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのカウンター攻撃状態クラス。
/// 敵の攻撃をパリィし、成功時にカウンター攻撃を行います。
/// </summary>
public class PlayerCounterAttack : EntityState
{
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="player">プレイヤー参照</param>
    /// <param name="stateMachine">ステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public PlayerCounterAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 状態開始時の処理。カウンター判定時間を設定します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理。カウンター判定と成功時の処理を行います。
    /// </summary>
    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        AudioManager.instance.Play("CounterSeccessful");

        // 攻撃範囲内のコライダーをチェック
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // ファイアボールを弾く
            if (hit.GetComponent<FireBall_Controller>() != null)
            {
                hit.GetComponent<FireBall_Controller>().FlipFireBall();
                stateTimer = 2f;
                player.anim.SetBool("SuccessfulCounterAttack", true);
            }
            // 敵のスタン可能な攻撃をカウンター
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 2f;
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }
        // タイマー終了またはアニメーション完了でアイドル状態へ
        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
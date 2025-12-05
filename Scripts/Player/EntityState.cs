using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの状態を表す抽象基底クラス。
/// 全ての具体的なプレイヤー状態はこのクラスを継承します。
/// </summary>
public abstract class EntityState
{
    /// <summary>状態が属するプレイヤー</summary>
    protected Player player;

    /// <summary>状態遷移を管理するステートマシン</summary>
    protected StateMachine stateMachine;

    /// <summary>物理演算用のRigidbody2D</summary>
    protected Rigidbody2D rb;

    /// <summary>水平入力値（-1, 0, 1）</summary>
    protected float xInput;

    /// <summary>垂直入力値（-1, 0, 1）</summary>
    protected float yInput;

    /// <summary>この状態に関連付けられたアニメーションパラメータ名</summary>
    private string animBoolName;

    /// <summary>状態内で使用するタイマー</summary>
    protected float stateTimer;

    /// <summary>アニメーションイベントからトリガーが呼ばれたかどうか</summary>
    protected bool triggerCalled;

    /// <summary>
    /// コンストラクタ。必要な参照を受け取ります。
    /// </summary>
    /// <param name="player">状態が属するプレイヤー</param>
    /// <param name="stateMachine">状態遷移を管理するステートマシン</param>
    /// <param name="animBoolName">アニメーションパラメータ名</param>
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    /// <summary>
    /// 状態に入った時に呼ばれます。アニメーションを開始します。
    /// </summary>
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    /// <summary>
    /// 毎フレーム呼ばれる更新処理。入力の取得とアニメーション更新を行います。
    /// </summary>
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        // 入力取得
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // y方向の速度をアニメーションに反映
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    /// <summary>
    /// 状態から出る時に呼ばれます。アニメーションを終了します。
    /// </summary>
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// アニメーションイベントから呼ばれるトリガー。
    /// アニメーション完了時などに使用します。
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

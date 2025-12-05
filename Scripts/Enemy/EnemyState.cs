using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の状態を表す基底クラス。
/// 全ての具体的な敵状態はこのクラスを継承します。
/// </summary>
public class EnemyState
{
    /// <summary>状態遷移を管理するステートマシン</summary>
    protected EnemyStateMachine stateMachine;

    /// <summary>状態が属する敵</summary>
    protected Enemy enemyBase;

    /// <summary>物理演算用のRigidbody2D</summary>
    protected Rigidbody2D rb;

    /// <summary>アニメーションイベントからトリガーが呼ばれたかどうか</summary>
    protected bool triggerCalled;

    /// <summary>この状態に関連付けられたアニメーションパラメータ名</summary>
    private string animBoolName;

    /// <summary>状態内で使用するタイマー</summary>
    protected float stateTimer;

    /// <summary>
    /// コンストラクタ。必要な参照を受け取ります。
    /// </summary>
    /// <param name="_enemyBase">状態が属する敵</param>
    /// <param name="_stateMachine">状態遷移を管理するステートマシン</param>
    /// <param name="_animBoolName">アニメーションパラメータ名</param>
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    /// <summary>
    /// 毎フレーム呼ばれる更新処理。タイマーを減少させます。
    /// </summary>
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    /// <summary>
    /// 状態に入った時に呼ばれます。アニメーションを開始します。
    /// </summary>
    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
    }

    /// <summary>
    /// 状態から出る時に呼ばれます。アニメーションを終了します。
    /// </summary>
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// アニメーションイベントから呼ばれるトリガー。
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

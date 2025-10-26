using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;

    // コンストラクタ：必要な参照を受け取る
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        // 入力取得
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // y方向の速度をアニメーションに反映
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false); // アニメーション終了
    }

    // アニメーションイベントから呼ばれるトリガー
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

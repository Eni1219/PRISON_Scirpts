using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シナー敵のスタン状態クラス。
/// スタン中は赤く点滅し、一定時間後にアイドル状態に戻ります。
/// </summary>
public class SinnerStun : EnemyState
{
    /// <summary>シナー敵の参照</summary>
    protected Enemy_Sinner enemy;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public SinnerStun(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Sinner enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    /// <summary>
    /// 状態開始時の処理。赤点滅を開始し、ノックバックを適用します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // 赤色点滅エフェクト開始
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);
        stateTimer = enemy.stunDuration;
        // スタン時のノックバック
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDir.x, enemy.stunDir.y);
    }

    /// <summary>
    /// 状態終了時の処理。赤点滅をキャンセルします。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0);
    }

    /// <summary>
    /// 毎フレームの更新処理。タイマー終了でアイドル状態へ。
    /// </summary>
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}

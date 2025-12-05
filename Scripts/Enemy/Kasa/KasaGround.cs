using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 笠敵の地上状態の基底クラス。
/// プレイヤー検出時に戦闘状態へ遷移します。
/// </summary>
public class KasaGround : EnemyState
{
    /// <summary>笠敵の参照</summary>
    protected Enemy_Kasa enemy;

    /// <summary>プレイヤーのTransform</summary>
    protected Transform player;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public KasaGround(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Kasa enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    /// <summary>
    /// 状態開始時の処理。プレイヤーを検索します。
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// 状態終了時の処理。
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 毎フレームの更新処理。プレイヤーを検出したら戦闘状態へ。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // プレイヤーを検出または近距離なら戦闘状態へ
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}

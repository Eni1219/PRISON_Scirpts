using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シナー敵の戦闘状態クラス。
/// プレイヤーを追跡し、攻撃距離に入ったら攻撃状態に遷移します。
/// </summary>
public class SinnerBattle : EnemyState
{
    /// <summary>プレイヤーのTransform</summary>
    protected Transform player;

    /// <summary>シナー敵の参照</summary>
    protected Enemy_Sinner enemy;

    /// <summary>移動方向（-1 or 1）</summary>
    private int moveDir;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public SinnerBattle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Sinner _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
    /// 攻撃可能かどうかをチェックします（クールダウン確認）。
    /// </summary>
    /// <returns>攻撃可能な場合true</returns>
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        Debug.Log("Attack CD");
        return false;
    }

    /// <summary>
    /// 毎フレームの更新処理。プレイヤーを追跡し、攻撃距離で攻撃します。
    /// </summary>
    public override void Update()
    {
        base.Update();
        // プレイヤーを検出している場合
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            // 攻撃距離に入ったら攻撃
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            // プレイヤーを見失ったらアイドル状態へ
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                stateMachine.ChangeState(enemy.idleState);
        }

        // プレイヤーの方向に向かって移動
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;
        enemy.SetVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);
    }
}

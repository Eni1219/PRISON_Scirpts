using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵用のステートマシンクラス。
/// 敵の現在の状態を管理し、状態間の遷移を制御します。
/// </summary>
public class EnemyStateMachine
{
    /// <summary>現在アクティブな状態</summary>
    public EnemyState currentState { get; private set; }

    /// <summary>
    /// ステートマシンを初期状態で初期化します。
    /// </summary>
    /// <param name="_startState">開始時の状態</param>
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    /// <summary>
    /// 現在の状態から新しい状態に遷移します。
    /// </summary>
    /// <param name="_newState">遷移先の状態</param>
    public void ChangeState(EnemyState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}

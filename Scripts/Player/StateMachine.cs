using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートマシンパターンの中核クラス。
/// 現在の状態を管理し、状態間の遷移を制御します。
/// </summary>
public class StateMachine
{
    /// <summary>現在アクティブな状態</summary>
    public EntityState currentState { get; private set; }

    /// <summary>
    /// ステートマシンを初期状態で初期化します。
    /// </summary>
    /// <param name="startState">開始時の状態</param>
    public void Initialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    /// <summary>
    /// 現在の状態から新しい状態に遷移します。
    /// </summary>
    /// <param name="newState">遷移先の状態</param>
    public void ChangeState(EntityState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

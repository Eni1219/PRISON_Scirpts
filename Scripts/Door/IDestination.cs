using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動先を定義するインターフェース。
/// ドアやテレポートポイントなどが実装します。
/// </summary>
public interface IDestination
{
    /// <summary>
    /// プレイヤーを移動先に移動させます。
    /// </summary>
    /// <param name="player">移動するプレイヤーのTransform</param>
    void Enter(Transform player);

    /// <summary>移動先の一意な識別子</summary>
    string Id { get; }
}

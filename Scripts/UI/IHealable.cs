using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回復能力を持つオブジェクトが実装するインターフェース。
/// 回復バーUIとの連携に使用します。
/// </summary>
public interface IHealable
{
    /// <summary>回復の最大使用回数</summary>
    int maxHealCount { get; }

    /// <summary>回復の現在使用可能回数</summary>
    int currentHealCount { get; }

    /// <summary>回復回数変更時に発火するイベント（現在回数, 最大回数）</summary>
    event System.Action<int, int> OnHealCountChanged;
}

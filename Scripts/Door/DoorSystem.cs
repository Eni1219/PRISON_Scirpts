using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ドア（テレポート）システムを管理する静的クラス。
/// 移動先の登録・解除・移動処理を一元管理します。
/// </summary>
public class DoorSystem : MonoBehaviour
{
    /// <summary>登録された移動先の辞書（ID → IDestination）</summary>
    static readonly Dictionary<string, IDestination> _destination = new();

    /// <summary>
    /// 移動先を登録します。
    /// </summary>
    /// <param name="dest">登録する移動先</param>
    public static void Register(IDestination dest)
    {
        if (dest == null || string.IsNullOrEmpty(dest.Id)) return;
        _destination[dest.Id] = dest;
    }

    /// <summary>
    /// 移動先の登録を解除します。
    /// </summary>
    /// <param name="dest">解除する移動先</param>
    public static void Unregister(IDestination dest)
    {
        if (dest == null) return;
        _destination.Remove(dest.Id);
    }

    /// <summary>
    /// 指定したIDの移動先にプレイヤーを移動させます。
    /// </summary>
    /// <param name="destinationId">移動先のID</param>
    /// <param name="player">移動するプレイヤーのTransform</param>
    /// <returns>移動が成功した場合true、失敗した場合false</returns>
    public static bool Go(string destinationId, Transform player)
    {
        // 移動先が存在する場合は移動を実行
        if (destinationId != null && _destination.TryGetValue(destinationId, out var dest))
        {
            dest.Enter(player);
            return true;
        }
        Debug.LogWarning($"[DoorSystem] Destination not found: {destinationId}");
        return false;
    }
}

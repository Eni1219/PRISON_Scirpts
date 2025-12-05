
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのスポーン地点を管理するクラス。
/// 部屋間移動時のテレポート先として使用されます。
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    /// <summary>スポーンポイントの一意な識別子</summary>
    public string spawnId;

    /// <summary>登録されたスポーンポイントの辞書（ID → SpawnPoint）</summary>
    static readonly Dictionary<string, SpawnPoint> _byId = new();

    /// <summary>
    /// Awake時にオブジェクト名を設定します。
    /// </summary>
    void Awake()
    {
        // インスペクターで設定されたIDに基づいてオブジェクト名を更新
        gameObject.name = string.IsNullOrEmpty(spawnId) ? gameObject.name : $"Spawn_{spawnId}";
    }

    /// <summary>
    /// オブジェクト有効化時に辞書に登録します。
    /// </summary>
    void OnEnable()
    {
        if (!string.IsNullOrEmpty(spawnId)) _byId[spawnId] = this;
        Debug.Log($"[SpawnPoint] Registered id={spawnId} at {transform.position}");
    }

    /// <summary>
    /// オブジェクト無効化時に辞書から削除します。
    /// </summary>
    void OnDisable()
    {
        if (!string.IsNullOrEmpty(spawnId))
        {
            _byId.Remove(spawnId);
            Debug.Log($"[SpawnPoint] Unregistered id={spawnId}");
        }
    }

    /// <summary>
    /// 指定したIDのスポーンポイントを検索します。
    /// </summary>
    /// <param name="id">検索するスポーンポイントのID</param>
    /// <returns>見つかった場合はTransform、見つからない場合はnull</returns>
    public static Transform Find(string id)
        => id != null && _byId.TryGetValue(id, out var sp) ? sp.transform : null;
}

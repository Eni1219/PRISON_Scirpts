
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnId;
    static readonly Dictionary<string, SpawnPoint> _byId = new();


    void Awake()
    {
        // 可选：在 Inspector 里更好找
        gameObject.name = string.IsNullOrEmpty(spawnId) ? gameObject.name : $"Spawn_{spawnId}";
    }
    void OnEnable()
    {
        if (!string.IsNullOrEmpty(spawnId)) _byId[spawnId] = this;
        Debug.Log($"[SpawnPoint] Registered id={spawnId} at {transform.position}");
    }
    void OnDisable()
    {
        if (!string.IsNullOrEmpty(spawnId))
        {
            _byId.Remove(spawnId);
            Debug.Log($"[SpawnPoint] Unregistered id={spawnId}");
        }
    }
    public static Transform Find(string id)
        => id != null && _byId.TryGetValue(id, out var sp) ? sp.transform : null;
}

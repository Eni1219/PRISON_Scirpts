using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HeartBarとIHealthの接続を行うバインダークラス。
/// シーン上でHeartBarとヘルスソースをインスペクターで接続します。
/// </summary>
public class HeartBarBinder : MonoBehaviour
{
    /// <summary>バインドするHeartBarコンポーネント</summary>
    [SerializeField] private HeartBar heartBar;

    /// <summary>ヘルスデータのソース（IHealthを実装するMonoBehaviour）</summary>
    [SerializeField] private MonoBehaviour healthSource;

    /// <summary>
    /// 初期化処理。HeartBarとヘルスソースをバインドします。
    /// </summary>
    void Start()
    {
        if (heartBar == null)
            heartBar = GetComponent<HeartBar>();
        // ヘルスソースがIHealthを実装している場合にバインド
        if (healthSource != null && healthSource is IHealth h)
            heartBar.Bind(h);
    }
}

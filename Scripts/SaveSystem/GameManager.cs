using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の状態を管理するシングルトンクラス。
/// リスポーン地点の管理など、シーンを跨いで保持される情報を管理します。
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>シングルトンインスタンス（どこからでもアクセス可能）</summary>
    public static GameManager instance;

    /// <summary>プレイヤーのリスポーン地点</summary>
    public Vector3 respawnPoint;

    /// <summary>
    /// Awake時にシングルトンの初期化を行います。
    /// </summary>
    private void Awake()
    {
        // シングルトンパターンの実装
        if (instance == null)
        {
            instance = this;
            // シーン遷移時も破棄されない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初期化処理。プレイヤーの初期位置をリスポーン地点に設定します。
    /// </summary>
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            respawnPoint = player.transform.position;
        }
    }

    /// <summary>
    /// リスポーン地点を設定します。
    /// </summary>
    /// <param name="newPoint">新しいリスポーン地点の座標</param>
    public void SetRespawnPoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;
    }
}

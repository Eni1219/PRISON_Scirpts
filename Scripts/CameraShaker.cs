using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cinemachineを使用したカメラシェイク機能を提供するシングルトンクラス。
/// 攻撃ヒット時や特定のイベント発生時にカメラを揺らすために使用します。
/// </summary>
public class CameraShaker : MonoBehaviour
{
    /// <summary>シングルトンインスタンス（どこからでもアクセス可能）</summary>
    public static CameraShaker instance;

    /// <summary>標準的なカメラシェイク用のImpulseSource</summary>
    [SerializeField] private CinemachineImpulseSource impulseSource;

    /// <summary>小さいカメラシェイク用のImpulseSource</summary>
    [SerializeField] private CinemachineImpulseSource smallShake;

    /// <summary>
    /// Awake時にシングルトンの初期化とImpulseSourceの取得を行います。
    /// </summary>
    void Awake()
    {
        // シングルトンパターンの実装
        if (instance == null)
            instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    /// <summary>
    /// 標準的なカメラシェイクを発生させます。
    /// 大きな衝撃や重要なイベント時に使用します。
    /// </summary>
    public void GenerateShake()
    {
        impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// 小さいカメラシェイクを発生させます。
    /// 軽い衝撃や小規模なヒット時に使用します。
    /// </summary>
    public void GenerateSmallShake()
    {
        smallShake.GenerateImpulse();
    }

}

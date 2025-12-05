using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HealBarとIHealableの接続を行うバインダークラス。
/// シーン上でHealBarと回復ソースをインスペクターで接続します。
/// </summary>
public class HealBarBinder : MonoBehaviour
{
    /// <summary>バインドするHealBarコンポーネント</summary>
    [SerializeField] private HealBar healBar;

    /// <summary>回復データのソース（IHealableを実装するMonoBehaviour）</summary>
    [SerializeField] private MonoBehaviour healSource;

    /// <summary>
    /// 初期化処理。HealBarと回復ソースをバインドします。
    /// </summary>
    private void Start()
    {
        // 回復ソースがIHealableを実装している場合にバインド
        if (healSource != null && healSource is IHealable h)
            healBar.Bind(h);
    }
}

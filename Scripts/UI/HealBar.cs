using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 回復アイテムの残り使用回数を表示するUIクラス。
/// IHealableインターフェースにバインドして、回復回数の変化を視覚的に表示します。
/// </summary>
public class HealBar : MonoBehaviour
{
    /// <summary>使用可能な回復アイテムのスプライト</summary>
    [Header("Sprites")]
    [SerializeField] private Sprite healFull;

    /// <summary>使用済み回復アイテムのスプライト</summary>
    [SerializeField] private Sprite healEmpty;

    /// <summary>回復アイテム画像のプレハブ</summary>
    [Header("Layout")]
    [SerializeField] private Image healPrefab;

    /// <summary>回復アイテム画像を配置するコンテナ</summary>
    [SerializeField] private Transform container;

    /// <summary>生成された回復アイテム画像のリスト</summary>
    private readonly List<Image> heals = new List<Image>();

    /// <summary>バインドされた回復ソース</summary>
    private IHealable boundHealable;

    /// <summary>
    /// 回復ソースをバインドします。回復回数変更イベントを購読します。
    /// </summary>
    /// <param name="healable">バインドする回復ソース</param>
    public void Bind(IHealable healable)
    {
        // 既存のバインドを解除
        if (boundHealable != null)
            boundHealable.OnHealCountChanged -= Render;
        boundHealable = healable;
        // 新しいソースをバインド
        if (boundHealable != null)
        {
            ResizeHeals(boundHealable.maxHealCount);
            Render(boundHealable.currentHealCount, boundHealable.maxHealCount);
            boundHealable.OnHealCountChanged += Render;
        }
    }

    /// <summary>
    /// 最大回復回数に応じて画像の数を調整します。
    /// </summary>
    /// <param name="maxCount">最大回復回数</param>
    private void ResizeHeals(int maxCount)
    {
        // 必要数まで生成
        while (heals.Count < maxCount)
        {
            var img = Instantiate(healPrefab, container);
            img.enabled = true;
            heals.Add(img);
        }
        // 必要数以外は非表示
        for (int i = 0; i < heals.Count; i++)
            heals[i].gameObject.SetActive(i < maxCount);
    }

    /// <summary>
    /// 回復回数に応じて表示を更新します。
    /// </summary>
    /// <param name="cur">現在の回復回数</param>
    /// <param name="max">最大回復回数</param>
    private void Render(int cur, int max)
    {
        ResizeHeals(max);
        for (int i = 0; i < heals.Count; i++)
        {
            // 使用可能ならfull、使用済みならempty
            heals[i].sprite = (i < cur) ? healFull : healEmpty;
        }
    }
}

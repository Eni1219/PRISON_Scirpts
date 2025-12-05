using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ハート型のHPバーを表示するUIクラス。
/// IHealthインターフェースにバインドして、HPの変化を視覚的に表示します。
/// </summary>
public class HeartBar : MonoBehaviour
{
    /// <summary>満タンのハートスプライト</summary>
    [Header("Sprites")]
    [SerializeField] private Sprite heartFull;

    /// <summary>空のハートスプライト</summary>
    [SerializeField] private Sprite heartEmpty;

    /// <summary>ハート画像のプレハブ</summary>
    [Header("Layout")]
    [SerializeField] private Image heartPrefab;

    /// <summary>ハート画像を配置するコンテナ</summary>
    [SerializeField] private Transform container;

    /// <summary>1つのハートで表すHP量</summary>
    [SerializeField] private int hpPerHeart = 4;

    /// <summary>生成されたハート画像のリスト</summary>
    private readonly List<Image> hearts = new List<Image>();

    /// <summary>バインドされたヘルスソース</summary>
    private IHealth boundHealth;

    /// <summary>
    /// ヘルスソースをバインドします。HP変更イベントを購読します。
    /// </summary>
    /// <param name="health">バインドするヘルスソース</param>
    public void Bind(IHealth health)
    {
        // 既存のバインドを解除
        if (boundHealth != null)
        {
            boundHealth.OnHealthChanged -= Render;
            boundHealth.OnDied -= OnDied;
        }

        boundHealth = health;

        // 新しいソースをバインド
        if (boundHealth != null)
        {
            ResizeHearts(boundHealth.maxHealth);
            Render(boundHealth.currentHealth, boundHealth.maxHealth);
            boundHealth.OnHealthChanged += Render;
            boundHealth.OnDied += OnDied;
        }
    }

    /// <summary>
    /// オブジェクト破棄時にイベント購読を解除します。
    /// </summary>
    private void OnDestroy()
    {
        if (boundHealth != null)
        {
            boundHealth.OnHealthChanged -= Render;
            boundHealth.OnDied -= OnDied;
        }
    }

    /// <summary>
    /// 死亡時のコールバック（現在は空実装）。
    /// </summary>
    private void OnDied()
    {

    }

    /// <summary>
    /// 最大HPに応じてハート画像の数を調整します。
    /// </summary>
    /// <param name="maxHp">最大HP</param>
    private void ResizeHearts(int maxHp)
    {
        int need = Mathf.CeilToInt(maxHp / (float)hpPerHeart);
        // 必要数まで生成
        while (hearts.Count < need)
        {
            var img = Instantiate(heartPrefab, container);
            img.enabled = true;
            hearts.Add(img);
        }

        // 必要数以外は非表示
        for (int i = 0; i < hearts.Count; i++)
            hearts[i].gameObject.SetActive(i < need);
    }

    /// <summary>
    /// HPに応じてハートの表示を更新します。
    /// </summary>
    /// <param name="cur">現在HP</param>
    /// <param name="max">最大HP</param>
    private void Render(int cur, int max)
    {
        ResizeHearts(max);

        int fullHearts = cur / hpPerHeart;
        // 半分ハートがあるかどうか（偶数hpPerHeartの場合）
        bool hasHalf = (cur % hpPerHeart) == (hpPerHeart / 2) && (hpPerHeart % 2 == 0);

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < fullHearts) hearts[i].sprite = heartFull;
            else if (i == fullHearts && hasHalf) hearts[i].sprite = heartEmpty;
            else hearts[i].sprite = heartEmpty;
        }
    }
}


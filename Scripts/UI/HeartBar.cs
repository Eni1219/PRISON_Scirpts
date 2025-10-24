using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartEmpty;

    [Header("Layout")]
    [SerializeField] private Image heartPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private int hpPerHeart = 4;

    private readonly List<Image>hearts= new List<Image>();
    private IHealth boundHealth;
    public void Bind(IHealth health)
    {
        if (boundHealth != null)
        {
            boundHealth.OnHealthChanged -= Render;
            boundHealth.OnDied -= OnDied;
        }

        boundHealth = health;

        if (boundHealth != null)
        {
            ResizeHearts(boundHealth.maxHealth);
            Render(boundHealth.currentHealth, boundHealth.maxHealth);
            boundHealth.OnHealthChanged += Render;
            boundHealth.OnDied += OnDied;
        }
    }

    private void OnDestroy()
    {
        if (boundHealth != null)
        {
            boundHealth.OnHealthChanged -= Render;
            boundHealth.OnDied -= OnDied;
        }
    }

    private void OnDied()
    {
        
    }

    private void ResizeHearts(int maxHp)
    {
        int need = Mathf.CeilToInt(maxHp / (float)hpPerHeart);
        while (hearts.Count < need)
        {
            var img = Instantiate(heartPrefab, container);
            img.enabled = true;
            hearts.Add(img);
        }

        for (int i = 0; i < hearts.Count; i++)
            hearts[i].gameObject.SetActive(i < need);
    }

    private void Render(int cur, int max)
    {
        ResizeHearts(max);

        int fullHearts = cur / hpPerHeart;
        bool hasHalf = (cur % hpPerHeart) == (hpPerHeart / 2) && (hpPerHeart % 2 == 0);

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < fullHearts) hearts[i].sprite = heartFull;
            else if (i == fullHearts && hasHalf) hearts[i].sprite = heartEmpty;
            else hearts[i].sprite = heartEmpty;
        }
    }
}


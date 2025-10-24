using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealBar : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite healFull;
    [SerializeField] private Sprite healEmpty;

    [Header("Layout")]
    [SerializeField] private Image healPrefab;
    [SerializeField] private Transform container;
    private readonly List<Image>heals= new List<Image>();
    private IHealable boundHealable;

    public void Bind(IHealable healable)
    {
        if (boundHealable != null)
            boundHealable.OnHealCountChanged -= Render;
        boundHealable = healable;
        if(boundHealable != null)
        {
            ResizeHeals(boundHealable.maxHealCount);
            Render(boundHealable.currentHealCount, boundHealable.maxHealCount);
            boundHealable.OnHealCountChanged += Render;
        }
    }
    private void ResizeHeals(int maxCount)
    {
        while(heals.Count<maxCount)
        {
            var img=Instantiate(healPrefab,container);
            img.enabled = true;
            heals.Add(img);
        }
        for(int i = 0; i < heals.Count; i++)
            heals[i].gameObject.SetActive(i<maxCount);
    }
    private void Render(int cur,int max)
    {
        ResizeHeals (max);
        for (int i = 0; i < heals.Count; i++)
        {
            heals[i].sprite = (i < cur) ? healFull : healEmpty;
        }
    }
}

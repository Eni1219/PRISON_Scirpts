using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBarBinder : MonoBehaviour
{
    [SerializeField] private HealBar healBar;
    [SerializeField] private MonoBehaviour healSource;

    private void Start()
    {
        if (healSource != null && healSource is IHealable h)
            healBar.Bind(h);
        
    }
}

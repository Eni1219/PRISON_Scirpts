using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBarBinder : MonoBehaviour
{
    [SerializeField] private HeartBar heartBar;
    [SerializeField] private MonoBehaviour healthSource;
    void Start()
    {
        if(heartBar == null)
            heartBar = GetComponent<HeartBar>();
        if (healthSource != null && healthSource is IHealth h)
            heartBar.Bind(h);
       
    }

}

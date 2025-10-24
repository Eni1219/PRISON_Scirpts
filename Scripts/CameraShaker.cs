using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;
    [SerializeField]private CinemachineImpulseSource impulseSource;
    [SerializeField] private CinemachineImpulseSource smallShake;

    void Awake()
    {
        if(instance==null)
            instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void GenerateShake()
    {
        impulseSource.GenerateImpulse();
    }
    public void GenerateSmallShake()
    {
        smallShake.GenerateImpulse();
    }

}

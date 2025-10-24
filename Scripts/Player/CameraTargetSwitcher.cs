using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTargetSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public Transform player;
    public Transform copyPlayer;
    public void FollowReal()
    {
        vcam.Follow = player;
    }
    public void FollowCopy()
    {
        vcam.Follow= copyPlayer;
    }
}

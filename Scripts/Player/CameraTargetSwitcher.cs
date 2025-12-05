using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// カメラの追従対象を切り替えるクラス。
/// カットシーン中のダミープレイヤーと実際のプレイヤー間で切り替えます。
/// </summary>
public class CameraTargetSwitcher : MonoBehaviour
{
    /// <summary>対象の仮想カメラ</summary>
    public CinemachineVirtualCamera vcam;

    /// <summary>実際のプレイヤーのTransform</summary>
    public Transform player;

    /// <summary>カットシーン用ダミープレイヤーのTransform</summary>
    public Transform copyPlayer;

    /// <summary>
    /// カメラの追従対象を実際のプレイヤーに切り替えます。
    /// </summary>
    public void FollowReal()
    {
        vcam.Follow = player;
    }

    /// <summary>
    /// カメラの追従対象をダミープレイヤーに切り替えます。
    /// </summary>
    public void FollowCopy()
    {
        vcam.Follow = copyPlayer;
    }
}

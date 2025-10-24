using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject copyPlayer;
    public GameObject HPPanel;
    public Vector3 realSpawnPos;

    public void SwitchBack()
    {

        copyPlayer.SetActive(false);
        player.transform.position = realSpawnPos;
        player.SetActive(true);
        HPPanel.SetActive(true);
    }
}

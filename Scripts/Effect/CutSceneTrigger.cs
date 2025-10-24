using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{

    public PlayableDirector director;
    public GameObject HPPanel;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player came");
            player.SetActive(false);
            HPPanel.SetActive(false);
            director.Play();
        }
    }
}

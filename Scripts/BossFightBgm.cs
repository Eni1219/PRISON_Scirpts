using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightBgm : MonoBehaviour
{
    bool isPlaying=false;
    bool hasPlayedTrigger=false;

    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!hasPlayedTrigger)
        {
            hasPlayedTrigger = true;
            if(AudioManager.instance!=null)
            {
            AudioManager.instance.Play("BossFightBgm");

            }
            isPlaying = true;
        }
    }

    public void ResetTrigger()
    {
        hasPlayedTrigger = false;
    }

}

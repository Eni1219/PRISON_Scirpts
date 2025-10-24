using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartRoomEffectTrigger : MonoBehaviour
{
    public GameObject square;
    public GameObject NPC;
    public GameObject NPC_Dead;
    public GameObject bonfire;
    public GameObject bonfire2;
    public AudioSource audioSource;
    public AudioClip clip;
    private bool hasTriggerd = false;

    [Header("Effect Info")]
    public float lockDuration = 2f;    
    public float shakeDuration = 10f; 
    public float shakeMagnitude = 50f; 

    public CameraShaker shaker;
    // Start is called before the first frame update
    void Start()
    {
        bonfire.SetActive(true);
        bonfire2 .SetActive(false);
        square.SetActive(true);
        NPC.SetActive(true);
        NPC_Dead.SetActive(false);
        CameraShaker shaker = GetComponent<CameraShaker>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")&&!hasTriggerd)
        {
            hasTriggerd = true;
            bonfire.SetActive(false);
            bonfire2.SetActive(true);
            square.SetActive(false);
            NPC.SetActive(false);
            NPC_Dead.SetActive(true);
            audioSource.Play();

            shaker.GenerateShake();
        }
    }
  
      
}

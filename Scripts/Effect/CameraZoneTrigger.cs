using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private CinemachineVirtualCamera fixCam;
    [SerializeField] private int highPriority = 20;
    [SerializeField]private int lowPriority = 5;

    [SerializeField] private NPCRunner npc;
    private bool hasTriggered=false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;
            
        hasTriggered = true;
            if(fixCam != null)
            {
                fixCam.Priority = highPriority;
                ui.SetActive(false);
            }
            if(npc != null)
            {
            npc.StartRunning();

            }    
        
    }
}

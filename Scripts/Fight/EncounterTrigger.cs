using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EncounterTrigger : MonoBehaviour
{
    public EncounterManager encounterManager;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            if (encounterManager != null)
                encounterManager.StartEncounter();
        }
    }
    public void ResetTrigger()
    {
        isTriggered = false;
        Debug.Log("True Now");
    }
}
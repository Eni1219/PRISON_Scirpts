using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public Gate[] gates;
    public GameObject[] enemies;

    private bool encounterStarted = false;
    private bool encounterCompleted = false;

    void Start()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            enemy.SetActive(false);

            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
                e.OnDeath += CheckEnemies; 
        }
    }

    public void StartEncounter()
    {
        if (encounterStarted) return; 
        encounterStarted = true;
        encounterCompleted = false;

        foreach (var gate in gates)
        {
            if (gate != null)
                gate.CloseGate();
        }

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            enemy.SetActive(true);
            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null)
                stats.HealToFull(); //敵全回復
        }
    }

    private void CheckEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null && stats.currentHealth > 0)
                return; 
        }

        encounterCompleted = true;
        encounterStarted = false; 

        foreach (var gate in gates)
        {
            if (gate != null)
                gate.OpenGate();
        }
    }

    public void HandleRespawn()
    {
        encounterCompleted = false;
        encounterStarted = false;
      
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null)
                stats.HealToFull();
            if(enemy.CompareTag("Boss"))
            {
                Debug.Log("Boss can");
                enemy.SetActive(true);
            }
            else
            {
                enemy.SetActive(false);
            }
        }

        foreach (var gate in gates)
        {
            if (gate != null)
                gate.OpenGate(true);
        }
      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyRespawnUtil
{
    public static void RestoreAll()
    {
        var all = Object.FindObjectsOfType<EnemyStats>(true);
        foreach (var e in all)
        {
            if (!e.gameObject.activeSelf)
                e.gameObject.SetActive(true);

            e.RestForRespawn();
        }
        var encounterManagers = Object.FindObjectsOfType<EncounterManager>();
        foreach (var manager in encounterManagers)
        {
            manager.HandleRespawn();
        }
    }
}


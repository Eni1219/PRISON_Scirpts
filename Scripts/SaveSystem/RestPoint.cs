using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPoint : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F;
    public float fade = 0.35f;
    public float restHold = 2f;
    public bool healOnRest = true;

    bool can;
    private Player player;
    void Reset() { GetComponent<Collider2D>().isTrigger = true; }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        can = true;
        player = c.GetComponent<Player>();

    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        can = false;
        player = null;

    }

    void Update()
    {
       
        if (!can || player == null) return;

        if (Input.GetKeyDown(interactKey))
        {
            GameManager.instance.SetRespawnPoint(transform.position);
            player.StartRest(fade, restHold, healOnRest);
            if(healOnRest)
            {
            player.stats.HealToFull();
            player.RestoreAllHeals();
            }
            EnemyRespawnUtil.RestoreAll();

        }
    }
}

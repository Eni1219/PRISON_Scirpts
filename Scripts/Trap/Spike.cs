using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Trap Damage")]
    public int damage = 4;
    [Header("CD")]
    public float coolDown = 1f;
    private float lastHitTime = -999f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Time.time>=lastHitTime+coolDown)
            {
             PlayerStats stats = collision.GetComponent<PlayerStats>();
             if(stats != null)
                {
                 stats.TakeDamage(damage);
                }
             lastHitTime = Time.time;
            }
        }
    }
}

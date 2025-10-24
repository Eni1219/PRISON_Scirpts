using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ProjectileController : MonoBehaviour
{

    [Header("Targeting / Damage")]
    [SerializeField] private string targetLayer = "Player";  
    [SerializeField] private int damage = 10;                 
    private CharacterStats ownerStats;                

    [Header("Movement")]
    [SerializeField] private float xVelocity = 10f;
    [SerializeField] private bool canMove = true;

    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 8f;              
    private float spawnTime;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D rb;

    private int facingDir = 1;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        spawnTime = Time.time;
    }

    void Update()
    {
        if (canMove && rb)
            rb.velocity = new Vector2(xVelocity*facingDir, rb.velocity.y);

     
        if (Time.time >= spawnTime + lifeTime)
            Destroy(gameObject);
    }

    public void SetUpBossProjectile(float speed, CharacterStats bossStats)
    {
        xVelocity = Mathf.Abs(speed);
        ownerStats = bossStats;

        facingDir = speed > 0 ? 1 : -1;
        if(facingDir==1)
        {
            transform.Rotate(0,180,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;

        if (otherLayer == LayerMask.NameToLayer(targetLayer))
        {
            var targetStats = other.GetComponent<CharacterStats>();
            if (ownerStats && targetStats)
                ownerStats.DoDamage(targetStats);
            else if (targetStats)
                targetStats.TakeDamage(damage); 

            Destroy(gameObject);
            return;
        }

        if (otherLayer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            return;
        }
    }

}



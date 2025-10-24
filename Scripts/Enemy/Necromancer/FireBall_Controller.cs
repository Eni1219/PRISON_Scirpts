using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Controller : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string targetLayer="Player";
    [SerializeField] private float xVelocity;
    private int facingDir = 1;
    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private CharacterStats myStats;

    [SerializeField] private Rigidbody2D rb;
 
    void Update()
    {
        if(canMove)
        rb.velocity=new Vector2(xVelocity*facingDir,rb.velocity.y);
    }
    public void SetUpFireBall(float _speed,CharacterStats _myStats)
    {
        xVelocity = Mathf.Abs(_speed);
        myStats = _myStats;
        facingDir = _speed > 0 ? 1 : -1;
        if(facingDir==1)
            transform.Rotate(0,180,0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            myStats.DoDamage(collision.GetComponent<CharacterStats>());
            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }
    private void StuckInto(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject);
    }
    public void FlipFireBall()
    {
        if (flipped)
            return;
        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayer = "Enemy";
    }
}

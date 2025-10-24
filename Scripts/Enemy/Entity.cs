using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("KnockBack Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField]protected float knockbackDuration;
    protected bool isKnocked;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask GroundLayer;
    public int facingDir{ get; private set; } = 1;
    public  bool facingRight = true;
    [SerializeField] private float flipCD = 0.1f;
    private float lastFlipTime;
    #region Components
    public Animator anim { get; private set; }
    public EntityFX fx { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public SpriteRenderer sr { get; private set; }

    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }
    #endregion

    // Start is called before the first frame update
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        fx=GetComponent<EntityFX>();  
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        stats = GetComponentInChildren<CharacterStats>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {

    }
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked=false;
    }

    #region Velocity
    public void ZeroVelocity()
    {
        if (isKnocked)
            return; 
        rb.velocity = new Vector2(0, 0);
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if(isKnocked)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, GroundLayer);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, GroundLayer);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);
    }
 
    #endregion
    #region Flip
    public virtual void Flip()
    {
        if (Time.time - lastFlipTime < flipCD)
            return;
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        lastFlipTime = Time.time;
    }
    public virtual void FlipController(float _x)

    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
    public virtual void Die()
    {

    }
}

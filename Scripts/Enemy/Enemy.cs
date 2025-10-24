using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy :Entity
{
    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDir;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;


    [SerializeField]protected LayerMask PlayerLayerMask;
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    [Header("Attack info")]
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector]public float lastTimeAttacked;

    public event Action OnDeath;
    public EnemyStateMachine stateMachine { get; private set; }


   protected override void Awake()
    {
        base.Awake();
        stateMachine=new EnemyStateMachine();
    }
    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
     
    }
 
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned=true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    public virtual bool CanBeStunned()
    {
        if(canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void AnimationFinishTrigger()=>stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected()=>Physics2D.Raycast(wallCheck.position,Vector2.right*facingDir,20,PlayerLayerMask);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x+attackDistance*facingDir,transform.position.y));
    }
    public virtual void ResetToIdle()
    {
        isKnocked = false;
        if (rb) rb.velocity = Vector2.zero;
        if (capsuleCollider) capsuleCollider.enabled = true;
    }
    public override void Die()
    {
        base.Die();
        OnDeath?.Invoke();
    }
    public virtual void Respawn()
    {
        isKnocked = false;
      

        if (rb != null)
            rb.velocity = Vector2.zero;
        if (capsuleCollider != null)
            capsuleCollider.enabled = true;

        if (anim != null)
            anim.SetBool("Die", false);
    }
    public virtual void AnimationSpecialAttackTrigger()
    {

    }
    public virtual void BossProjectileAttackTrigger()
    {

    }
    }

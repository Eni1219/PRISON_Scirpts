using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IHealable
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip footStepSE;
    [Header("Heal Info")]
    [SerializeField] private int _maxHealCount = 3;
    private int _currentHealCount;
    public int maxHealCount => _maxHealCount;
    public int currentHealCount => _currentHealCount;
    public event System.Action<int, int> OnHealCountChanged;


    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;
    private float dashTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }
    public bool isBusy { get; private set; }
    public bool isDead { get; private set; }

    #region Skills
    public bool canDoubleJump{ get; private set; }
    public bool doubleJumpUsed { get; set; }
    public bool canCounter {  get; private set; }

    public bool canWallSlide {  get; private set; }
    #endregion

    #region States
    private StateMachine stateMachine;
    public Player_Idle idleState { get; private set; }
    public Player_Move moveState { get; private set; }
    public Player_Air airState { get; private set; }
    public Player_Jump jumpState { get; private set; }
    public Player_Dash dashState { get; private set; }
    public Player_WallSlide wallSlideState { get; private set; }
    public Player_WallJump wallJumpState { get; private set; }
    public Player_PrimaryAttack primaryAttack {  get; private set; }
    public PlayerCounterAttack counterAttack { get; private set; }
    public Player_Death deadState { get; private set; }
    public Player_Rest restState { get; private set; }
    public Player_Heal healState { get; private set; }  
    public PlayerAirCombo airCombo { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine();
        audioSource = GetComponent<AudioSource>();
        idleState = new Player_Idle(this, stateMachine, "Idle");
        moveState = new Player_Move(this, stateMachine, "Move");
        jumpState = new Player_Jump(this, stateMachine, "Jump");
        airState = new Player_Air(this, stateMachine, "Jump");
        dashState = new Player_Dash(this, stateMachine, "Dash");
        wallSlideState = new Player_WallSlide(this, stateMachine, "WallSlide");
        wallJumpState = new Player_WallJump(this, stateMachine, "WallJump");
        primaryAttack = new Player_PrimaryAttack(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttack(this, stateMachine, "CounterAttack");
        deadState = new Player_Death(this, stateMachine, "Die");
        restState = new Player_Rest(this, stateMachine, "Rest");
        healState = new Player_Heal(this, stateMachine, "Heal");
        airCombo = new PlayerAirCombo(this, stateMachine, "AirAttack");
    }
        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
            _currentHealCount=_maxHealCount;
            canWallSlide = false;
            canCounter = false;
            OnHealCountChanged?.Invoke(_currentHealCount,_maxHealCount);

        }
       protected override void Update()
        {
            base.Update();
        if (isDead) return;
          
            stateMachine.currentState.Update();
            CheckDashInput();
        
        }
   public bool TryUseHeal()
    {
        if(_currentHealCount>0)
        {
            _currentHealCount--;
            OnHealCountChanged?.Invoke(_currentHealCount, _maxHealCount);
            return true;
        }
        return false;
    }
    public void RestoreAllHeals()
    {
        _currentHealCount = _maxHealCount;
        OnHealCountChanged?.Invoke(_currentHealCount, _maxHealCount);
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public void AnimationTrigger()=>stateMachine.currentState.AnimationFinishTrigger();

    public void UnlockCounter()
    {
        canCounter = true;
    }
    public void UnlockDoubleJump()
    {
        canDoubleJump = true;
    }
    public void Landed()
    {
        doubleJumpUsed = false;
    }
    
    public void UnlockWallSlide()
    {
        canWallSlide = true;
    }
    private void CheckDashInput()
    {
        if (isWallDetected())
            return;
        dashTimer-=Time.deltaTime;
      
        if(Input.GetKeyDown(KeyCode.LeftShift)&&dashTimer<0)
        {
            dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir==0)
                dashDir=facingDir;
            stateMachine.ChangeState(dashState);
        }
    }
    public override void Die()
    {
        base.Die(); 
        stateMachine.ChangeState(deadState);
        AudioManager.instance.Stop("BossFightBgm");
        isDead = true;
    }
    public void Respawn()
    {
        isDead = false;
        transform.position = GameManager.instance.respawnPoint;
        gameObject.layer = LayerMask.NameToLayer("Player");
        BossFightBgm bossTrigger=FindObjectOfType<BossFightBgm>();
        if (bossTrigger != null)
        {
            bossTrigger.ResetTrigger();
        }
        EncounterTrigger trigger = FindObjectOfType<EncounterTrigger>();
        if (trigger != null)
        {
            trigger.ResetTrigger();

        }
        stats.HealToFull();
        EnemyRespawnUtil.RestoreAll();
        stateMachine.ChangeState(idleState);
    }
    public void StartRest(float fadeTime,float holdTime,bool healToFull)
    {
        restState.Setup(fadeTime, holdTime, healToFull);
        stateMachine.ChangeState(restState);
    }
    public void HealEffect()
    {

    }
   
}

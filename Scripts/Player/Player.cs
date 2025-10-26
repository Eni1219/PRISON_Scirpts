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
    public event System.Action<int, int> OnHealCountChanged; // 回復回数の変更通知イベント

    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;
    private float dashTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; } // ダッシュ方向
    public bool isBusy { get; private set; }   // 一時的に操作不可かどうか
    public bool isDead { get; private set; }   // 死亡状態かどうか

    #region Skills
    public bool canDoubleJump { get; private set; } // 二段ジャンプ可能か
    public bool doubleJumpUsed { get; set; }
    public bool canCounter { get; private set; }    // カウンター可能か
    public bool canWallSlide { get; private set; }  // 壁スライド可能か
    #endregion

    #region States
    private StateMachine stateMachine;
    // 各状態のインスタンス（ステートパターン）
    public Player_Idle idleState { get; private set; }
    public Player_Move moveState { get; private set; }
    public Player_Air airState { get; private set; }
    public Player_Jump jumpState { get; private set; }
    public Player_Dash dashState { get; private set; }
    public Player_WallSlide wallSlideState { get; private set; }
    public Player_WallJump wallJumpState { get; private set; }
    public Player_PrimaryAttack primaryAttack { get; private set; }
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

        // 各状態の初期化（アニメーション名と紐付け）
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
        stateMachine.Initialize(idleState); // 初期状態を Idle に設定
        _currentHealCount = _maxHealCount;
        canWallSlide = false;
        canCounter = false;
        OnHealCountChanged?.Invoke(_currentHealCount, _maxHealCount);
    }

    protected override void Update()
    {
        base.Update();
        if (isDead) return;

        stateMachine.currentState.Update(); // 現在の状態を更新
        CheckDashInput(); // ダッシュ入力の確認
    }

    // 回復使用処理（残り回数があれば true を返す）
    public bool TryUseHeal()
    {
        if (_currentHealCount > 0)
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

    // 一定時間操作不可にする（例：回復中など）
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void UnlockCounter() => canCounter = true;
    public void UnlockDoubleJump() => canDoubleJump = true;
    public void Landed() => doubleJumpUsed = false;
    public void UnlockWallSlide() => canWallSlide = true;

    // ダッシュ入力の確認と処理
    private void CheckDashInput()
    {
        if (isWallDetected()) return;

        dashTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
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

    // リスポーン処理（位置・状態・敵の復活など）
    public void Respawn()
    {
        isDead = false;
        transform.position = GameManager.instance.respawnPoint;
        gameObject.layer = LayerMask.NameToLayer("Player");

        BossFightBgm bossTrigger = FindObjectOfType<BossFightBgm>();
        if (bossTrigger != null) bossTrigger.ResetTrigger();

        EncounterTrigger trigger = FindObjectOfType<EncounterTrigger>();
        if (trigger != null) trigger.ResetTrigger();

        stats.HealToFull();
        EnemyRespawnUtil.RestoreAll();
        stateMachine.ChangeState(idleState);
    }

    // 休憩状態に移行（フェードや回復処理）
    public void StartRest(float fadeTime, float holdTime, bool healToFull)
    {
        restState.Setup(fadeTime, holdTime, healToFull);
        stateMachine.ChangeState(restState);
    }

    public void HealEffect()
    {
        // 回復時のエフェクト処理（未実装）
    }
}

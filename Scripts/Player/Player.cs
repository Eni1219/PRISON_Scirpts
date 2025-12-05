/// <summary>
/// プレイヤーキャラクターを制御するメインクラス。
/// ステートマシンパターンで各状態（アイドル、移動、攻撃など）を管理し、
/// IHealableインターフェースを実装して回復機能を提供します。
/// </summary>
public class Player : Entity, IHealable
{
    /// <summary>各コンボ攻撃段階での移動量配列</summary>
    [Header("Attack Details")]
    public Vector2[] attackMovement;

    /// <summary>カウンター攻撃の持続時間（秒）</summary>
    public float counterAttackDuration = .2f;

    /// <summary>効果音再生用のAudioSource</summary>
    [Header("SE")]
    public AudioSource audioSource;

    /// <summary>足音のオーディオクリップ</summary>
    public AudioClip footStepSE;

    /// <summary>回復の最大使用回数</summary>
    [Header("Heal Info")]
    [SerializeField] private int _maxHealCount = 3;

    /// <summary>回復の現在使用可能回数</summary>
    private int _currentHealCount;

    /// <summary>回復の最大使用回数（読み取り専用）</summary>
    public int maxHealCount => _maxHealCount;

    /// <summary>回復の現在使用可能回数（読み取り専用）</summary>
    public int currentHealCount => _currentHealCount;

    /// <summary>回復回数が変更された時に発火するイベント</summary>
    public event System.Action<int, int> OnHealCountChanged;

    /// <summary>移動速度</summary>
    [Header("Move info")]
    public float moveSpeed = 12f;

    /// <summary>ジャンプ力</summary>
    public float jumpForce;

    /// <summary>ダッシュのクールダウン時間</summary>
    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;

    /// <summary>ダッシュクールダウンタイマー</summary>
    private float dashTimer;

    /// <summary>ダッシュ速度</summary>
    public float dashSpeed;

    /// <summary>ダッシュの持続時間</summary>
    public float dashDuration;

    /// <summary>ダッシュ方向（-1 or 1）</summary>
    public float dashDir { get; private set; }

    /// <summary>一時的に操作不可かどうか（攻撃後の硬直など）</summary>
    public bool isBusy { get; private set; }

    /// <summary>死亡状態かどうか</summary>
    public bool isDead { get; private set; }

    #region Skills
    /// <summary>二段ジャンプが解放されているかどうか</summary>
    public bool canDoubleJump { get; private set; }

    /// <summary>二段ジャンプを使用済みかどうか（着地でリセット）</summary>
    public bool doubleJumpUsed { get; set; }

    /// <summary>カウンター攻撃が解放されているかどうか</summary>
    public bool canCounter { get; private set; }

    /// <summary>壁スライドが解放されているかどうか</summary>
    public bool canWallSlide { get; private set; }
    #endregion

    #region States
    /// <summary>プレイヤーのステートマシン</summary>
    private StateMachine stateMachine;

    /// <summary>アイドル状態</summary>
    public Player_Idle idleState { get; private set; }

    /// <summary>移動状態</summary>
    public Player_Move moveState { get; private set; }

    /// <summary>空中状態</summary>
    public Player_Air airState { get; private set; }

    /// <summary>ジャンプ状態</summary>
    public Player_Jump jumpState { get; private set; }

    /// <summary>ダッシュ状態</summary>
    public Player_Dash dashState { get; private set; }

    /// <summary>壁スライド状態</summary>
    public Player_WallSlide wallSlideState { get; private set; }

    /// <summary>壁ジャンプ状態</summary>
    public Player_WallJump wallJumpState { get; private set; }

    /// <summary>通常攻撃状態</summary>
    public Player_PrimaryAttack primaryAttack { get; private set; }

    /// <summary>カウンター攻撃状態</summary>
    public PlayerCounterAttack counterAttack { get; private set; }

    /// <summary>死亡状態</summary>
    public Player_Death deadState { get; private set; }

    /// <summary>休憩状態</summary>
    public Player_Rest restState { get; private set; }

    /// <summary>回復状態</summary>
    public Player_Heal healState { get; private set; }

    /// <summary>空中コンボ攻撃状態</summary>
    public PlayerAirCombo airCombo { get; private set; }
    #endregion

    /// <summary>
    /// Awake時の初期化処理。ステートマシンと各状態を初期化します。
    /// </summary>
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

    /// <summary>
    /// Start時の初期化処理。初期状態をアイドルに設定します。
    /// </summary>
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        _currentHealCount = _maxHealCount;
        canWallSlide = false;
        canCounter = false;
        OnHealCountChanged?.Invoke(_currentHealCount, _maxHealCount);
    }

    /// <summary>
    /// 毎フレームの更新処理。現在の状態を更新し、ダッシュ入力を確認します。
    /// </summary>
    protected override void Update()
    {
        base.Update();
        if (isDead) return;

        stateMachine.currentState.Update();
        CheckDashInput();
    }

    /// <summary>
    /// 回復を試みます。残り回数があれば使用し、trueを返します。
    /// </summary>
    /// <returns>回復が使用できた場合true、残り回数がない場合false</returns>
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

    /// <summary>
    /// 回復回数を最大まで回復します。
    /// </summary>
    public void RestoreAllHeals()
    {
        _currentHealCount = _maxHealCount;
        OnHealCountChanged?.Invoke(_currentHealCount, _maxHealCount);
    }

    /// <summary>
    /// 一定時間操作不可にするコルーチン。攻撃後の硬直などに使用します。
    /// </summary>
    /// <param name="_seconds">操作不可の時間（秒）</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    /// <summary>
    /// アニメーション完了トリガーを現在の状態に伝達します。
    /// </summary>
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    /// <summary>カウンター攻撃スキルを解放します。</summary>
    public void UnlockCounter() => canCounter = true;

    /// <summary>二段ジャンプスキルを解放します。</summary>
    public void UnlockDoubleJump() => canDoubleJump = true;

    /// <summary>着地時の処理。二段ジャンプの使用状態をリセットします。</summary>
    public void Landed() => doubleJumpUsed = false;

    /// <summary>壁スライドスキルを解放します。</summary>
    public void UnlockWallSlide() => canWallSlide = true;

    /// <summary>
    /// ダッシュ入力を確認し、条件を満たせばダッシュ状態に遷移します。
    /// </summary>
    private void CheckDashInput()
    {
        // 壁検出中はダッシュ不可
        if (isWallDetected()) return;

        dashTimer -= Time.deltaTime;

        // Shiftキー押下かつクールダウン完了でダッシュ
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            // 入力がなければ現在の向きにダッシュ
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }

    /// <summary>
    /// プレイヤーの死亡処理。死亡状態に遷移し、BGMを停止します。
    /// </summary>
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        AudioManager.instance.Stop("BossFightBgm");
        isDead = true;
    }

    /// <summary>
    /// プレイヤーのリスポーン処理。位置と状態をリセットし、敵も復活させます。
    /// </summary>
    public void Respawn()
    {
        isDead = false;
        transform.position = GameManager.instance.respawnPoint;
        gameObject.layer = LayerMask.NameToLayer("Player");

        // ボストリガーをリセット
        BossFightBgm bossTrigger = FindObjectOfType<BossFightBgm>();
        if (bossTrigger != null) bossTrigger.ResetTrigger();

        // エンカウンタートリガーをリセット
        EncounterTrigger trigger = FindObjectOfType<EncounterTrigger>();
        if (trigger != null) trigger.ResetTrigger();

        // HPを全回復し、敵を復活させる
        stats.HealToFull();
        EnemyRespawnUtil.RestoreAll();
        stateMachine.ChangeState(idleState);
    }

    /// <summary>
    /// 休憩状態に移行します。フェードや回復処理を行います。
    /// </summary>
    /// <param name="fadeTime">フェード時間</param>
    /// <param name="holdTime">休憩保持時間</param>
    /// <param name="healToFull">HPを全回復するかどうか</param>
    public void StartRest(float fadeTime, float holdTime, bool healToFull)
    {
        restState.Setup(fadeTime, holdTime, healToFull);
        stateMachine.ChangeState(restState);
    }

    /// <summary>
    /// 回復時のエフェクト処理（現在は未実装）。
    /// </summary>
    public void HealEffect()
    {
        // 回復時のエフェクト処理（未実装）
    }
}

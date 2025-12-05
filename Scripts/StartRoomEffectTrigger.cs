using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ゲーム開始時の演出（地震エフェクト、オブジェクト切り替え等）を制御するトリガークラス。
/// プレイヤーが特定のエリアに入ると、NPCの死亡演出や焚き火の変化などを行います。
/// </summary>
public class StartRoomEffectTrigger : MonoBehaviour
{
    /// <summary>演出で消すオブジェクト（四角い障害物など）</summary>
    public GameObject square;

    /// <summary>生存状態のNPCオブジェクト</summary>
    public GameObject NPC;

    /// <summary>死亡状態のNPCオブジェクト</summary>
    public GameObject NPC_Dead;

    /// <summary>演出前の焚き火オブジェクト</summary>
    public GameObject bonfire;

    /// <summary>演出後の焚き火オブジェクト（変化後）</summary>
    public GameObject bonfire2;

    /// <summary>演出時の効果音再生用AudioSource</summary>
    public AudioSource audioSource;

    /// <summary>演出時に再生する効果音クリップ</summary>
    public AudioClip clip;

    /// <summary>既にトリガーが発動したかどうか</summary>
    private bool hasTriggerd = false;

    /// <summary>プレイヤーの操作をロックする時間（秒）</summary>
    [Header("Effect Info")]
    public float lockDuration = 2f;

    /// <summary>カメラシェイクの持続時間（秒）</summary>
    public float shakeDuration = 10f;

    /// <summary>カメラシェイクの強さ</summary>
    public float shakeMagnitude = 50f;

    /// <summary>カメラシェイク制御用コンポーネント</summary>
    public CameraShaker shaker;

    /// <summary>
    /// 初期化処理。各オブジェクトの初期表示状態を設定します。
    /// </summary>
    void Start()
    {
        // 初期状態の設定
        bonfire.SetActive(true);
        bonfire2.SetActive(false);
        square.SetActive(true);
        NPC.SetActive(true);
        NPC_Dead.SetActive(false);
        CameraShaker shaker = GetComponent<CameraShaker>();
    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時に演出を開始します。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤータグを持ち、まだ発動していない場合のみ処理
        if (other.CompareTag("Player") && !hasTriggerd)
        {
            hasTriggerd = true;
            // オブジェクトの表示切り替え
            bonfire.SetActive(false);
            bonfire2.SetActive(true);
            square.SetActive(false);
            NPC.SetActive(false);
            NPC_Dead.SetActive(true);
            // 効果音の再生
            audioSource.Play();
            // カメラシェイクの発動
            shaker.GenerateShake();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPCとの会話インタラクションを管理するクラス。
/// プレイヤーが範囲内でEキーを押すと会話を開始します。
/// </summary>
public class TalkInteract : MonoBehaviour
{
    /// <summary>インタラクト可能時に表示するボタンUI</summary>
    public GameObject button;

    /// <summary>表示する会話データ</summary>
    public DialogueData dialogueData;

    /// <summary>会話管理マネージャーの参照</summary>
    private DialogueManager dialogueManager;

    /// <summary>プレイヤーが範囲内にいるかどうか</summary>
    private bool PlayerInRange = false;

    /// <summary>
    /// 初期化処理。DialogueManagerの取得とUIの初期設定を行います。
    /// </summary>
    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
            Debug.LogError("Cant find DialogueManager");
        if (button != null)
            button.SetActive(false);
    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
            // インタラクトボタンを表示
            if (button != null)
                button.SetActive(true);
        }
    }

    /// <summary>
    /// プレイヤーがトリガー領域から出た時の処理。
    /// </summary>
    /// <param name="other">トリガーから離れたコライダー</param>
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            // インタラクトボタンを非表示
            if (button != null)
                button.SetActive(false);
        }
    }

    /// <summary>
    /// 毎フレームの更新処理。プレイヤーの入力を監視します。
    /// </summary>
    void Update()
    {
        // プレイヤーが範囲内でEキーを押したら会話開始
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartTalk();
        }
    }

    /// <summary>
    /// 会話を開始します。
    /// </summary>
    public void StartTalk()
    {
        if (dialogueManager != null && dialogueData != null)
        {
            // ボタンを非表示にして会話を開始
            if (button != null)
                button.SetActive(false);

            dialogueManager.StartDialogue(dialogueData);
        }
    }
}

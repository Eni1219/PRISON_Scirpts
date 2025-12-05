using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ゲーム内の会話システムを管理するクラス。
/// 会話データの表示、テキストのタイピングエフェクト、会話の進行を制御します。
/// </summary>
public class DialogueManager : MonoBehaviour
{
    /// <summary>会話UIパネル</summary>
    [Header("UI")]
    public GameObject dialoguePanel;

    /// <summary>会話テキストを表示するTextMeshProコンポーネント</summary>
    public TextMeshProUGUI dialogueText;

    /// <summary>次の会話行に進むキー</summary>
    public KeyCode nextLineKey = KeyCode.Return;

    /// <summary>テキスト表示速度（秒/文字）</summary>
    public float textSpeed = .05f;

    /// <summary>現在表示中の会話データ</summary>
    private DialogueData currentDialogue;

    /// <summary>現在の会話行のインデックス</summary>
    private int currentLineIndex = 0;

    /// <summary>テキストタイピング中かどうか</summary>
    private bool isTyping = false;

    /// <summary>タイピングエフェクト用のコルーチン参照</summary>
    private Coroutine typingCoroutine;

    /// <summary>
    /// 初期化処理。会話パネルを非表示にします。
    /// </summary>
    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    /// <summary>
    /// 会話を開始します。
    /// </summary>
    /// <param name="dialogueData">表示する会話データ</param>
    public void StartDialogue(DialogueData dialogueData)
    {
        // 無効なデータの場合は処理しない
        if (dialogueData == null || dialogueData.dialogueLines.Length == 0)
            return;

        currentDialogue = dialogueData;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayLine();
    }

    /// <summary>
    /// 現在の行を表示します。タイピングエフェクトを開始します。
    /// </summary>
    void DisplayLine()
    {
        // 既存のタイピングコルーチンがあれば停止
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
        }
        string line = currentDialogue.dialogueLines[currentLineIndex];
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    /// <summary>
    /// テキストを1文字ずつ表示するタイピングエフェクトのコルーチン。
    /// </summary>
    /// <param name="line">表示するテキスト</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        // 1文字ずつ追加しながら表示
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentDialogue.textSpeed);
        }
        isTyping = false;
    }

    /// <summary>
    /// 次の会話行に進みます。タイピング中の場合は即座に全文を表示します。
    /// </summary>
    public void NextLine()
    {
        // タイピング中なら即座に全文表示
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue.dialogueLines[(currentLineIndex)];
            isTyping = false;
            return;
        }
        currentLineIndex++;
        // まだ次の行があれば表示、なければ終了
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            DisplayLine();
        }
        else
        {
            EndDialogue();
        }
    }

    /// <summary>
    /// 会話を終了し、UIを非表示にします。
    /// </summary>
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentDialogue = null;
        currentLineIndex = 0;
    }

    /// <summary>
    /// 毎フレームの更新処理。入力を監視して会話を進行させます。
    /// </summary>
    void Update()
    {
        // パネルがアクティブな場合の入力処理
        if (dialoguePanel.activeSelf)
        {
            // タイピング中にスペースキーで即時表示
            if (isTyping && Input.GetKeyDown(KeyCode.Space))
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
                isTyping = false;
                return;
            }
        }
        // タイピング完了後、次の行へ進むキー入力を確認
        if (!isTyping && (Input.GetKeyDown(nextLineKey)))
        {
            NextLine();
        }
    }
}

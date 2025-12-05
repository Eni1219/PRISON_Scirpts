using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 会話データを保持するシリアライズ可能なクラス。
/// インスペクターで会話の内容と表示設定を行います。
/// </summary>
[System.Serializable]
public class DialogueData
{
    /// <summary>会話の各行を保持する配列</summary>
    [Header("DialogueSetting")]
    public string[] dialogueLines;

    /// <summary>テキスト表示速度（秒/文字）</summary>
    public float textSpeed = .05f;
}

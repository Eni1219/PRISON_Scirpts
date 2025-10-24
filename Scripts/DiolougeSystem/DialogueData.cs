using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    [Header("DialogueSetting")]
    public string[] dialogueLines;
    public float textSpeed = .05f;
}

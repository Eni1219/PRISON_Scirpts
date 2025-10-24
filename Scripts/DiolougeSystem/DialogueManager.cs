using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public KeyCode nextLineKey=KeyCode.Return;

    public float textSpeed = .05f;
    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private bool isTyping=false;
    private Coroutine typingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
    public void StartDialogue(DialogueData dialogueData)
    {
        if(dialogueData==null||dialogueData.dialogueLines.Length==0)
            return;

        currentDialogue = dialogueData;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayLine();

    }
    void DisplayLine()
    {
        if(currentLineIndex<currentDialogue.dialogueLines.Length)
        {
            if(typingCoroutine!=null)
                StopCoroutine(typingCoroutine);
        }
        string line=currentDialogue.dialogueLines[currentLineIndex];
        typingCoroutine=StartCoroutine(TypeLine(line)); 
    }
   
    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentDialogue.textSpeed);
        }
        isTyping=false;
    }
    public void NextLine()
    {
        if(isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text=currentDialogue.dialogueLines[(currentLineIndex)];
            isTyping = false;
            return;
        }
        currentLineIndex++;
        if(currentLineIndex<currentDialogue.dialogueLines.Length)
        {
            DisplayLine();
        }
        else
        {
            EndDialogue();
        }
    }
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentDialogue = null;
        currentLineIndex = 0;
    }
    void Update()
    {
        if(dialoguePanel.activeSelf)
        {
        if(isTyping&&Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(typingCoroutine) ;
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
            isTyping = false;
            return;
        }

        }
        if(!isTyping&&(Input.GetKeyDown(nextLineKey)))
        {
            NextLine(); 
        }
    }
}

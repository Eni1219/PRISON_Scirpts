using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : MonoBehaviour
{
    public GameObject button;
    public DialogueData dialogueData;
    private DialogueManager dialogueManager;
    private bool PlayerInRange = false;

    private void Start()
    {
        dialogueManager=FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
            Debug.LogError("Cant find DialogueManager");
        if(button!=null)
            button.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInRange = true;
            if(button!=null)
                button.SetActive(true);
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInRange=false;
            if(button!= null)
                button.SetActive(false);
        }
    }
    void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartTalk();
        }
     

    }
    public void StartTalk()
    {
        if(dialogueManager!=null&&dialogueData!=null)
        {
            if(button!=null)
                button.SetActive(false);

            dialogueManager.StartDialogue(dialogueData);
        }
    }
}

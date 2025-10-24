using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum skillType
{
doubleJump,
counter,
wallSlide,
attackBoost,
hpBoost
}

public class Item : MonoBehaviour
{
    public skillType skillUnlock;
    public GameObject button;
    public int boostHPAmount = 4;
    public int boostATKAmount = 5;
    private bool playerNearby;
    private Player player;


    public GameObject UI; 

    private void Start()
    {
        if(UI!=null)
        UI.SetActive(false);
        playerNearby = false;
        if(button != null )
            button.SetActive( false );
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&playerNearby)
        {
            if(player!=null)
            {
            UnlockSkill(player);
            Destroy(gameObject,2.1f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {

            playerNearby=true;
            player=other.GetComponent<Player>();
            if(button!=null)
            button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            playerNearby = false;
        if(button!=null)
                button.SetActive(false);
        }
    }
    private void UnlockSkill(Player player)
    {
        PlayerStats playerStats=player.GetComponent<PlayerStats>();
        string message = "";

        Debug.Log($"Message: {message}");
        switch (skillUnlock)
        { 
           case skillType.doubleJump:
                player.UnlockDoubleJump();
                message = "���ФǶ��Υ�����ʹ�ÿ���";
                    break;

                case skillType.wallSlide:
                player.UnlockWallSlide();
                message = "�ڤ˻�����ܤˤʤ�";
                break;
                case skillType.attackBoost:
                if (playerStats != null)
                    playerStats.BoostAttack(boostATKAmount);
                message = "���������å�";
                break;
                case skillType.hpBoost:
                if(playerStats != null)
                    playerStats.BoostMaxHealth(boostHPAmount);
                message = "HP���ޥ��å�";
                break;
            case skillType.counter:
                player.UnlockCounter();
                message = "K�ǥѥꥣʹ�ÿ���";
                break;
        }
        if(UI!=null)
        {
            UI.SetActive(true);
            AudioManager.instance.Play("ItemPanel");
            var text = UI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if(text != null)
            {
                text.text=message;
            }
            Invoke(nameof(HideUI), 2f);
        }
    }
    private void HideUI()
    {
        if(UI != null) UI.SetActive(false);
    }
    

}

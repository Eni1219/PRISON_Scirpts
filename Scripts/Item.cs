using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// プレイヤーがアンロックできるスキルの種類を定義する列挙型。
/// </summary>
public enum skillType
{
    /// <summary>二段ジャンプスキル</summary>
    doubleJump,
    /// <summary>カウンター攻撃スキル</summary>
    counter,
    /// <summary>壁スライドスキル</summary>
    wallSlide,
    /// <summary>攻撃力ブーストアイテム</summary>
    attackBoost,
    /// <summary>HP最大値ブーストアイテム</summary>
    hpBoost
}

/// <summary>
/// プレイヤーがインタラクトして取得できるアイテム（スキルアンロックやステータスブースト）を管理するクラス。
/// プレイヤーが近づいてEキーを押すとスキルがアンロックされます。
/// </summary>
public class Item : MonoBehaviour
{
    /// <summary>このアイテムでアンロックされるスキルの種類</summary>
    public skillType skillUnlock;

    /// <summary>インタラクト可能時に表示するボタンUI</summary>
    public GameObject button;

    /// <summary>HPブースト時の増加量</summary>
    public int boostHPAmount = 4;

    /// <summary>攻撃力ブースト時の増加量</summary>
    public int boostATKAmount = 5;

    /// <summary>プレイヤーが近くにいるかどうか</summary>
    private bool playerNearby;

    /// <summary>近くにいるプレイヤーの参照</summary>
    private Player player;

    /// <summary>スキル取得時に表示するUI</summary>
    public GameObject UI;

    /// <summary>
    /// 初期化処理。UIとボタンを非表示にします。
    /// </summary>
    private void Start()
    {
        if (UI != null)
            UI.SetActive(false);
        playerNearby = false;
        if (button != null)
            button.SetActive(false);
    }

    /// <summary>
    /// 毎フレームの更新処理。プレイヤーがEキーを押した時にスキルをアンロックします。
    /// </summary>
    private void Update()
    {
        // プレイヤーが近くにいてEキーが押された場合
        if (Input.GetKeyDown(KeyCode.E) && playerNearby)
        {
            if (player != null)
            {
                UnlockSkill(player);
                // スキルアンロック後、アイテムを削除
                Destroy(gameObject, 2.1f);
            }
        }
    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.GetComponent<Player>();
            // インタラクトボタンを表示
            if (button != null)
                button.SetActive(true);
        }
    }

    /// <summary>
    /// プレイヤーがトリガー領域から出た時の処理。
    /// </summary>
    /// <param name="other">トリガーから離れたコライダー</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            playerNearby = false;
            // インタラクトボタンを非表示
            if (button != null)
                button.SetActive(false);
        }
    }

    /// <summary>
    /// スキルをアンロックし、UIにメッセージを表示します。
    /// </summary>
    /// <param name="player">スキルをアンロックするプレイヤー</param>
    private void UnlockSkill(Player player)
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        string message = "";

        Debug.Log($"Message: {message}");
        // スキルタイプに応じた処理を実行
        switch (skillUnlock)
        {
            case skillType.doubleJump:
                player.UnlockDoubleJump();
                message = "空中でジャンプ使用可能";
                break;

            case skillType.wallSlide:
                player.UnlockWallSlide();
                message = "壁に捕まる事になる";
                break;
            case skillType.attackBoost:
                if (playerStats != null)
                    playerStats.BoostAttack(boostATKAmount);
                message = "攻撃力アップ";
                break;
            case skillType.hpBoost:
                if (playerStats != null)
                    playerStats.BoostMaxHealth(boostHPAmount);
                message = "HPがマックスアップ";
                break;
            case skillType.counter:
                player.UnlockCounter();
                message = "Kでパリィ使用可能";
                break;
        }
        // UIにメッセージを表示
        if (UI != null)
        {
            UI.SetActive(true);
            AudioManager.instance.Play("ItemPanel");
            var text = UI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (text != null)
            {
                text.text = message;
            }
            Invoke(nameof(HideUI), 2f);
        }
    }

    /// <summary>
    /// UIを非表示にします。Invokeから呼び出されます。
    /// </summary>
    private void HideUI()
    {
        if (UI != null) UI.SetActive(false);
    }


}

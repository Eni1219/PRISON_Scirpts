using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カットシーン後にプレイヤーを切り替えるクラス。
/// ダミープレイヤーから実際のプレイヤーに制御を戻す際に使用します。
/// </summary>
public class PlayerSwitcher : MonoBehaviour
{
    /// <summary>実際のプレイヤーオブジェクト</summary>
    public GameObject player;

    /// <summary>カットシーン用のダミープレイヤー</summary>
    public GameObject copyPlayer;

    /// <summary>HPパネルUI</summary>
    public GameObject HPPanel;

    /// <summary>プレイヤーの復帰位置</summary>
    public Vector3 realSpawnPos;

    /// <summary>
    /// ダミープレイヤーから実際のプレイヤーに切り替えます。
    /// Timeline終了時などに呼び出されます。
    /// </summary>
    public void SwitchBack()
    {
        // ダミープレイヤーを非表示
        copyPlayer.SetActive(false);
        // 実際のプレイヤーを指定位置に移動して表示
        player.transform.position = realSpawnPos;
        player.SetActive(true);
        // HPパネルを再表示
        HPPanel.SetActive(true);
    }
}

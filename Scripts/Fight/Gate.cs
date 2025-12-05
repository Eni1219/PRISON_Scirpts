using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 開閉可能なゲート（扉）を制御するクラス。
/// 戦闘エンカウンター時に閉じ、クリア後に開きます。
/// </summary>
public class Gate : MonoBehaviour
{
    /// <summary>ゲートが開いている時の位置</summary>
    public Transform openPos;

    /// <summary>ゲートが閉じている時の位置</summary>
    public Transform closePos;

    /// <summary>ゲートの移動速度</summary>
    public float speed = 5f;

    /// <summary>現在の移動目標位置</summary>
    public Transform targetPos;

    /// <summary>
    /// 初期化処理。ゲートを開いた状態に設定します。
    /// </summary>
    private void Start()
    {
        targetPos = openPos;
        transform.position = openPos.position;
    }

    /// <summary>
    /// 毎フレームの更新処理。目標位置に向かって移動します。
    /// </summary>
    void Update()
    {
        if (targetPos != null)
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
    }

    /// <summary>
    /// ゲートを閉じます。
    /// </summary>
    /// <param name="silent">trueの場合、効果音を再生しない</param>
    public void CloseGate(bool silent = false)
    {
        if (closePos != null)
        {
            // 効果音を再生（silentでない場合）
            if (!silent)
                AudioManager.instance.PlayOneShot("DoorClose");
        }
        targetPos = closePos;
    }

    /// <summary>
    /// ゲートを開きます。
    /// </summary>
    /// <param name="silent">trueの場合、効果音を再生しない</param>
    public void OpenGate(bool silent = false)
    {
        if (openPos != null)
        {
            // 効果音を再生（silentでない場合）
            if (!silent)
                AudioManager.instance.PlayOneShot("DoorOpen");
        }
        targetPos = openPos;
    }
}

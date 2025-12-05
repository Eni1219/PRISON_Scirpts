using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPCが指定方向に走るアニメーション制御クラス。
/// カットシーンやイベント演出でNPCを移動させる際に使用します。
/// </summary>
public class NPCRunner : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] private float moveSpeed = 5f;

    /// <summary>走る方向ベクトル</summary>
    [SerializeField] private Vector2 runDir = new Vector2(1, 0);

    /// <summary>現在走り中かどうか</summary>
    private bool isRunnnig = false;

    /// <summary>アニメーターコンポーネント（未使用）</summary>
    private Animator animator;

    /// <summary>
    /// 毎フレームの更新処理。走り中であれば指定方向に移動します。
    /// </summary>
    void Update()
    {
        if (isRunnnig)
        {
            // 正規化した方向ベクトルに速度を掛けて移動
            transform.Translate(runDir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 走りを開始します。アニメーションも連動して再生されます。
    /// </summary>
    public void StartRunning()
    {
        isRunnnig = true;
        GetComponent<Animator>().SetBool("Running", true);
    }
}

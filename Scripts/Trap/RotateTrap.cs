using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転するトラップ（ノコギリなど）を制御するクラス。
/// 一定速度で常に回転し続けます。
/// </summary>
public class RotateTrap : MonoBehaviour
{
    /// <summary>回転速度（1秒間の回転数）</summary>
    [SerializeField] private float speed = 2f;

    /// <summary>
    /// 毎フレームの更新処理。Z軸周りに回転します。
    /// </summary>
    void Update()
    {
        // Z軸周りに回転（360度 × deltaTime × 速度）
        transform.Rotate(0, 0, 360 * Time.deltaTime * speed);
        // 効果音再生（現在はコメントアウト）
        //if (AudioManager.instance != null)
        //{
        //    AudioManager.instance.Play("Saw");
        //}
    }
}

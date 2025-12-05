using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スパイクトラップを制御するクラス。
/// プレイヤーが接触するとダメージを与えます。
/// </summary>
public class Spike : MonoBehaviour
{
    /// <summary>与えるダメージ量</summary>
    [Header("Trap Damage")]
    public int damage = 4;

    /// <summary>ダメージのクールダウン時間（秒）</summary>
    [Header("CD")]
    public float coolDown = 1f;

    /// <summary>最後にダメージを与えた時間</summary>
    private float lastHitTime = -999f;

    /// <summary>
    /// プレイヤーがトリガー領域に入った時にダメージを与えます。
    /// クールダウン中は連続ダメージを防ぎます。
    /// </summary>
    /// <param name="collision">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // クールダウンが終わっている場合のみダメージ
            if (Time.time >= lastHitTime + coolDown)
            {
                PlayerStats stats = collision.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.TakeDamage(damage);
                }
                lastHitTime = Time.time;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 複数のウェイポイント間を巡回移動するトラップクラス。
/// ノコギリや移動床などの動くトラップに使用します。
/// </summary>
public class WayPoint : MonoBehaviour
{
    /// <summary>巡回するウェイポイントの配列</summary>
    [SerializeField] private GameObject[] wayPoints;

    /// <summary>現在の目標ウェイポイントのインデックス</summary>
    private int currentWayPointIndex = 0;

    /// <summary>移動速度</summary>
    [SerializeField] private float speed = 2f;

    /// <summary>
    /// 毎フレームの更新処理。次のウェイポイントに向かって移動します。
    /// </summary>
    void Update()
    {
        // 現在のウェイポイントに到達したら次へ
        if (Vector2.Distance(wayPoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            currentWayPointIndex++;
            // 最後に達したら最初に戻る（ループ）
            if (currentWayPointIndex >= wayPoints.Length)
                currentWayPointIndex = 0;
        }
        // 目標ウェイポイントに向かって移動
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2点間を往復するエレベーターを制御するクラス。
/// ボタンで起動し、指定の速度で移動します。
/// </summary>
public class Elevator : MonoBehaviour
{
    /// <summary>移動の開始点（または終点）</summary>
    public Transform pointA;

    /// <summary>移動の終点（または開始点）</summary>
    public Transform pointB;

    /// <summary>エレベーターの移動速度</summary>
    public float speed = 2f;

    /// <summary>各端点での待機時間（秒）</summary>
    public float waitTime = 5f;

    /// <summary>エレベーターが起動中かどうか</summary>
    private bool isActive = false;

    /// <summary>現在の目標地点</summary>
    private Transform target;

    /// <summary>待機時間のカウンター</summary>
    private float waitCounter = 0f;

    /// <summary>
    /// 初期化処理。最初の目標地点を設定します。
    /// </summary>
    void Start()
    {
        target = pointB;
    }

    /// <summary>
    /// 毎フレームの更新処理。起動中であれば目標地点に向かって移動します。
    /// </summary>
    void Update()
    {
        if (!isActive) return;

        // 待機中の場合はカウントダウン
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            return;
        }

        // 目標地点に向かって移動
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 目標地点に到達したら反対側を次の目標に設定
        if (Vector2.Distance(transform.position, target.position) < .05f)
        {
            target = (target == pointA) ? pointB : pointA;
            waitCounter = waitTime;
        }
    }

    /// <summary>
    /// エレベーターを起動します。
    /// </summary>
    public void ActivateElevator()
    {
        isActive = true;
    }
}

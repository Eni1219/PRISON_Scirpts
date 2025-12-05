using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のリスポーン処理を行う静的ユーティリティクラス。
/// 休憩ポイントでの回復時やプレイヤー死亡時に全ての敵を復活させます。
/// </summary>
public static class EnemyRespawnUtil
{
    /// <summary>
    /// シーン内の全ての敵を復活させます。
    /// 非アクティブな敵もアクティブ化し、HPを全回復します。
    /// </summary>
    public static void RestoreAll()
    {
        // 全てのEnemyStats（非アクティブ含む）を取得
        var all = Object.FindObjectsOfType<EnemyStats>(true);
        foreach (var e in all)
        {
            // 非アクティブな敵をアクティブ化
            if (!e.gameObject.activeSelf)
                e.gameObject.SetActive(true);

            // リスポーン用のリセット処理
            e.RestForRespawn();
        }

        // エンカウンターマネージャーもリスポーン処理
        var encounterManagers = Object.FindObjectsOfType<EncounterManager>();
        foreach (var manager in encounterManagers)
        {
            manager.HandleRespawn();
        }
    }
}


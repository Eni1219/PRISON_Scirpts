using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戦闘エンカウンター（敵との遭遇戦）を管理するクラス。
/// 複数の敵とゲートを制御し、全ての敵を倒すとゲートが開きます。
/// </summary>
public class EncounterManager : MonoBehaviour
{
    /// <summary>エンカウンター時に閉じるゲートの配列</summary>
    public Gate[] gates;

    /// <summary>エンカウンター時に出現する敵の配列</summary>
    public GameObject[] enemies;

    /// <summary>エンカウンターが開始されたかどうか</summary>
    private bool encounterStarted = false;

    /// <summary>エンカウンターが完了したかどうか</summary>
    private bool encounterCompleted = false;

    /// <summary>
    /// 初期化処理。敵を非表示にし、死亡イベントを登録します。
    /// </summary>
    void Start()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            // 開始時は敵を非表示
            enemy.SetActive(false);

            // 敵の死亡イベントに監視処理を登録
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
                e.OnDeath += CheckEnemies;
        }
    }

    /// <summary>
    /// エンカウンターを開始します。ゲートを閉じ、敵を出現させます。
    /// </summary>
    public void StartEncounter()
    {
        // 既に開始済みの場合は何もしない
        if (encounterStarted) return;
        encounterStarted = true;
        encounterCompleted = false;

        // 全てのゲートを閉じる
        foreach (var gate in gates)
        {
            if (gate != null)
                gate.CloseGate();
        }

        // 全ての敵を出現させ、HPを全回復
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            enemy.SetActive(true);
            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null)
                stats.HealToFull(); // 敵全回復
        }
    }

    /// <summary>
    /// 敵が倒れた時に呼び出され、全滅したかチェックします。
    /// </summary>
    private void CheckEnemies()
    {
        // 生存している敵がいるかチェック
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null && stats.currentHealth > 0)
                return; // まだ生存している敵がいる
        }

        // 全滅した場合、ゲートを開く
        encounterCompleted = true;
        encounterStarted = false;

        foreach (var gate in gates)
        {
            if (gate != null)
                gate.OpenGate();
        }
    }

    /// <summary>
    /// プレイヤーのリスポーン時の処理。敵とゲートの状態をリセットします。
    /// </summary>
    public void HandleRespawn()
    {
        encounterCompleted = false;
        encounterStarted = false;

        // 敵の状態をリセット
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null)
                stats.HealToFull();

            // ボスは表示、通常敵は非表示
            if (enemy.CompareTag("Boss"))
            {
                Debug.Log("Boss can");
                enemy.SetActive(true);
            }
            else
            {
                enemy.SetActive(false);
            }
        }

        // ゲートを静かに開く（SE無し）
        foreach (var gate in gates)
        {
            if (gate != null)
                gate.OpenGate(true);
        }
    }
}

// RoomDestination.cs
using UnityEngine;
using System.Collections;
using Cinemachine;

/// <summary>
/// 部屋間のテレポート先を定義するクラス。
/// フェードエフェクト付きでプレイヤーを指定のスポーンポイントに移動させます。
/// </summary>
public class RoomDestination : MonoBehaviour, IDestination
{
    /// <summary>移動先の一意な識別子</summary>
    [SerializeField] string id;

    /// <summary>移動先のスポーンポイントID</summary>
    public string spawnPointId;

    /// <summary>移動先のカメラゾーンID（オプション）</summary>
    public string cameraZoneId;

    /// <summary>フェードエフェクトの時間（秒）</summary>
    public float fadeTime = 0.25f;

    /// <summary>移動先IDを取得するプロパティ</summary>
    public string Id => id;

    /// <summary>
    /// オブジェクト有効化時にDoorSystemに登録します。
    /// </summary>
    void OnEnable() => DoorSystem.Register(this);

    /// <summary>
    /// オブジェクト無効化時にDoorSystemから解除します。
    /// </summary>
    void OnDisable() => DoorSystem.Unregister(this);

    /// <summary>
    /// プレイヤーを移動先に移動させます。
    /// </summary>
    /// <param name="player">移動するプレイヤーのTransform</param>
    public void Enter(Transform player)
    {
        StartCoroutine(TeleportSequence(player));
        // スポーンポイントを検索してプレイヤーを移動
        var sp = SpawnPoint.Find(spawnPointId);
        if (sp && player)
        {
            player.position = sp.position;
        }
    }

    /// <summary>
    /// テレポートシーケンスのコルーチン。フェードアウト→移動→フェードインを実行します。
    /// </summary>
    /// <param name="player">移動するプレイヤーのTransform</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    IEnumerator TeleportSequence(Transform player)
    {
        // 画面をフェードアウト
        yield return ScreenFader.Instance.FadeOut(.5f);

        // 移動処理（現在はEnter内で実行）
        // var sp = SpawnPoint.Find(spawnPointId);
        // if (sp && player) player.position = sp.position;

        yield return null;

        // 画面をフェードイン
        yield return ScreenFader.Instance.FadeIn(10f);
    }
}

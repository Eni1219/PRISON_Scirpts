// RoomDestination.cs
using UnityEngine;
using System.Collections;
using Cinemachine;

public class RoomDestination : MonoBehaviour, IDestination
{
    [SerializeField] string id;
    public string spawnPointId; // 目标出生点
    public string cameraZoneId; // 目标相机边界（可留空）
    public float fadeTime = 0.25f;

    public string Id => id;

    void OnEnable() => DoorSystem.Register(this);
    void OnDisable() => DoorSystem.Unregister(this);

    public void Enter(Transform player)
    {
        StartCoroutine(TeleportSequence(player));
        var sp = SpawnPoint.Find(spawnPointId);
        if (sp && player)
        {
            player.position = sp.position;
        }
      
    }
    IEnumerator TeleportSequence(Transform player)
    {

        yield return ScreenFader.Instance.FadeOut(.5f);

        //var sp = SpawnPoint.Find(spawnPointId);
        //if (sp && player) player.position = sp.position;

        yield return null;

        yield return ScreenFader.Instance.FadeIn(10f);

    }
}

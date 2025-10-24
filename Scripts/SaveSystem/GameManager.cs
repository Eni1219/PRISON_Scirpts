using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector3 respawnPoint;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        if(player != null )
        {
            respawnPoint=player.transform.position;
        }
    }
    public void SetRespawnPoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;
    }
}

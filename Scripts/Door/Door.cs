using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string destinationId;
    public KeyCode interactKey = KeyCode.F;
    public string prompt = "F§«»Î§Î";
    public GameObject F;

    bool _can; 
    Transform _player;

    void Start()
    {
        Debug.Log($"[Door] Start - destinationId = '{destinationId}'");
        F.SetActive( false );
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))return;
        _can = true;
        _player=other.transform;
        F.SetActive( true );
 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _can=false;
        _player=null;
        F.SetActive( false );
  
    }
    private void Update()
    {
        if (_can && Input.GetKeyDown(interactKey) && _player)
        {
            var ok = DoorSystem.Go(destinationId, _player);
        }


    }
}

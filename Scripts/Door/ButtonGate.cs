using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGate : MonoBehaviour
{
    public Transform openPoint;
    public float speed = 2f;
    private bool isOpen=false;
 
    private void Update()
    {
        if (!isOpen) return;
        AudioManager.instance.Play("DoorOpen");
        
        transform.position = Vector2.MoveTowards(transform.position, openPoint.position, speed * Time.deltaTime);
        
    }
    public void GateOpen()
    {
        isOpen = true;
    }
}

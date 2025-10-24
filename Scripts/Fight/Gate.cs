using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Transform openPos;
    public Transform closePos;
    public float speed = 5f;
    public Transform targetPos;


    private void Start()
    {
        targetPos=openPos;
        transform.position = openPos.position;
    }
    void Update()
    {
        if(targetPos != null)
        transform.position=Vector3.MoveTowards(transform.position,targetPos.position,speed*Time.deltaTime);

    }
    public void CloseGate(bool silent=false)
    {
        if (closePos != null)
        {
            if(!silent)
            AudioManager.instance.PlayOneShot("DoorClose");
        }
        targetPos = closePos;
    }
    public void OpenGate(bool silent=false)

    {
        if(openPos!=null)
        {
            if(!silent)
            AudioManager.instance.PlayOneShot("DoorOpen");
        }
        targetPos = openPos;
    }
}

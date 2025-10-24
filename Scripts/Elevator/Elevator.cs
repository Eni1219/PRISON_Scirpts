using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 5f;
    private bool isActive = false;
    private Transform target;
    private float waitCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        target=pointB;
    }

    
    void Update()
    {
        if (!isActive) return;
        if(waitCounter>0)
        {
            waitCounter-=Time.deltaTime;
            return;
        }
        transform.position=Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, target.position)<.05f)
        {
            target=(target==pointA) ? pointB : pointA;
            waitCounter=waitTime;
        }
    }
    public void ActivateElevator()
    {
        isActive = true;
    }
}

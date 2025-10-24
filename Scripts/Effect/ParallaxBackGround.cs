using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float length;

    void Start()
    {
       
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distanceMoved=cam.transform.position.x*(1-parallaxEffect);
        float distanceToMove=cam.transform.position.x*parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if(distanceMoved > xPosition+length)
            xPosition=xPosition+length;
        else if(distanceMoved< xPosition-length)
            xPosition=xPosition-length;
    }
}

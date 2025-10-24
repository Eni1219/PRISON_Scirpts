using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrap : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
   
    void Update()
    {
     transform.Rotate(0,0,360*Time.deltaTime*speed);
        //if (AudioManager.instance != null)
        //{
        //    AudioManager.instance.Play("Saw");
        //}
    }
}

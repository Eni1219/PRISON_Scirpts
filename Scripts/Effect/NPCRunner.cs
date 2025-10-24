using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRunner : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 runDir = new Vector2(1, 0);
    private bool isRunnnig=false;
    private Animator animator;
    void Update()
    {
        if(isRunnnig)
        {
            transform.Translate(runDir.normalized*moveSpeed*Time.deltaTime);
        }
    }
    public void StartRunning()
    {
        isRunnnig = true;
        GetComponent<Animator>().SetBool("Running", true);
    }
}

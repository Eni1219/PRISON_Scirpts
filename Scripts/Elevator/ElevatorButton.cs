using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorButton : MonoBehaviour
{
    private Elevator elevator;
    private Vector3 originalPos;
    public float pressDepth = .2f;
    public float pressSpeed = 3f;
    private bool isPressed=false;

    public UnityEvent onPressed;
    void Start()
    {
        elevator = GetComponentInParent<Elevator>();
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = originalPos + (isPressed ? Vector3.down * pressDepth : Vector3.zero);
        transform.localPosition=Vector3.Lerp(transform.localPosition,targetPos,Time.deltaTime*pressSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Player"))
        {
            isPressed = true;
            onPressed.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
            other.transform.SetParent(null);
        }
    }
}

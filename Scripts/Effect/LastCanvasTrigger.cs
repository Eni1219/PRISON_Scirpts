using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LastCanvasTrigger : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private float fadeInDuration=1.5f;
    [SerializeField] private float waitTime = 2f;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null )
            canvasGroup = UI.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        UI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            UI.SetActive(true);
            StartCoroutine(FadeInAndLoadScene());
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private IEnumerator FadeInAndLoadScene()
    {
        UI.SetActive(true);
        float timer = 0f;
        while(timer<fadeInDuration)
        {
            canvasGroup.alpha=Mathf.Lerp(0,1,timer/fadeInDuration);
            timer+= Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("End");
    }
}

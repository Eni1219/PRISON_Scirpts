using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject deathPanel;
    private Player player;

    private void Awake()
    {
        if(instance==null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        player=FindObjectOfType<Player>();
        if(deathPanel!=null)
            deathPanel.SetActive(false);
    }
    public void ShowDeathUI()
    {
        if(deathPanel!=null)
        {
            StartCoroutine(ShowDeathUIAfterDelay(3f));
        }
    }
    private IEnumerator ShowDeathUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        deathPanel.SetActive(true);
        if(AudioManager.instance!=null)
        {
            Debug.Log("Try to play DeathBgm");
        AudioManager.instance.Play("DeathBgm");
        }
        else
        {
            Debug.LogError("Null");
        }
        Time.timeScale = 0f;
    }
    public void OnRespawnButtonClicked()
    {
        Time.timeScale = 1f;
        if(player!=null)
        {
            player.Respawn();
        }
        deathPanel.SetActive(false);
    }
    public void OnBackToTitleClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

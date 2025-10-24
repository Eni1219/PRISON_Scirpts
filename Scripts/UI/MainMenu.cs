using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button[] buttons;
    public GameObject cursor;
    private int currentSelection = 0;
    void Start()
    {
        UpdateCursorPosition();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection++;
            if (currentSelection >= buttons.Length)
            {
                currentSelection = 0;
            }
            UpdateCursorPosition();
        }
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentSelection--;
                if(currentSelection < 0)
                {
                    currentSelection=buttons.Length-1;
                }
                UpdateCursorPosition();
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                buttons[currentSelection].onClick.Invoke();
                Debug.Log("Enter");
            }
        
    }
    void UpdateCursorPosition()
    {
        Vector3 buttonPos = buttons[currentSelection].transform.position;
        cursor.transform.position = new Vector3(buttonPos.x - 100f, buttonPos.y, buttonPos.z);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

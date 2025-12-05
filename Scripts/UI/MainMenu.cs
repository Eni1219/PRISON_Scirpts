using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// メインメニューUIを管理するクラス。
/// キーボード操作によるメニュー選択とボタン実行を制御します。
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>選択可能なメニューボタンの配列</summary>
    public Button[] buttons;

    /// <summary>現在選択中のボタンを示すカーソルUI</summary>
    public GameObject cursor;

    /// <summary>現在選択中のボタンインデックス</summary>
    private int currentSelection = 0;

    /// <summary>
    /// 初期化処理。カーソル位置を初期化します。
    /// </summary>
    void Start()
    {
        UpdateCursorPosition();
    }

    /// <summary>
    /// 毎フレームの更新処理。キーボード入力を監視してメニュー操作を行います。
    /// </summary>
    void Update()
    {
        // 下キーで次の選択肢へ
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection++;
            // 最後まで行ったら最初に戻る
            if (currentSelection >= buttons.Length)
            {
                currentSelection = 0;
            }
            UpdateCursorPosition();
        }
        // 上キーで前の選択肢へ
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelection--;
            // 最初より前は最後に戻る
            if (currentSelection < 0)
            {
                currentSelection = buttons.Length - 1;
            }
            UpdateCursorPosition();
        }
        // スペースキーで決定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[currentSelection].onClick.Invoke();
            Debug.Log("Enter");
        }
    }

    /// <summary>
    /// カーソル位置を現在選択中のボタンに合わせて更新します。
    /// </summary>
    void UpdateCursorPosition()
    {
        Vector3 buttonPos = buttons[currentSelection].transform.position;
        cursor.transform.position = new Vector3(buttonPos.x - 100f, buttonPos.y, buttonPos.z);
    }

    /// <summary>
    /// ゲームを開始します（メインシーンをロード）。
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// ゲームを終了します。
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// タイトル画面に戻ります。
    /// </summary>
    public void BackToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

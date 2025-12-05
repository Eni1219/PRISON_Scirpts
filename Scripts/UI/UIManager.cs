using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体のUIを管理するシングルトンクラス。
/// 死亡画面の表示やリスポーン、タイトル画面への遷移を制御します。
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>シングルトンインスタンス（どこからでもアクセス可能）</summary>
    public static UIManager instance;

    /// <summary>死亡時に表示するパネル</summary>
    [SerializeField] private GameObject deathPanel;

    /// <summary>プレイヤーの参照</summary>
    private Player player;

    /// <summary>
    /// Awake時にシングルトンの初期化を行います。
    /// </summary>
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 初期化処理。プレイヤーの取得と死亡パネルの初期化を行います。
    /// </summary>
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    /// <summary>
    /// 死亡UIを表示します。遅延後に表示されます。
    /// </summary>
    public void ShowDeathUI()
    {
        if (deathPanel != null)
        {
            StartCoroutine(ShowDeathUIAfterDelay(3f));
        }
    }

    /// <summary>
    /// 指定時間後に死亡UIを表示するコルーチン。
    /// ゲームを一時停止し、死亡BGMを再生します。
    /// </summary>
    /// <param name="delay">表示までの遅延時間（秒）</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator ShowDeathUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        deathPanel.SetActive(true);
        // 死亡BGMを再生
        if (AudioManager.instance != null)
        {
            Debug.Log("Try to play DeathBgm");
            AudioManager.instance.Play("DeathBgm");
        }
        else
        {
            Debug.LogError("Null");
        }
        // ゲームを一時停止
        Time.timeScale = 0f;
    }

    /// <summary>
    /// リスポーンボタンクリック時の処理。
    /// ゲームを再開し、プレイヤーをリスポーンさせます。
    /// </summary>
    public void OnRespawnButtonClicked()
    {
        Time.timeScale = 1f;
        if (player != null)
        {
            player.Respawn();
        }
        deathPanel.SetActive(false);
    }

    /// <summary>
    /// タイトルに戻るボタンクリック時の処理。
    /// ゲームを再開し、メインメニューシーンをロードします。
    /// </summary>
    public void OnBackToTitleClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム終了時のエンディング演出を制御するトリガークラス。
/// プレイヤーが特定エリアに入るとフェードイン後、エンディングシーンへ遷移します。
/// </summary>
public class LastCanvasTrigger : MonoBehaviour
{
    /// <summary>フェードインするUI（エンディング画面）</summary>
    [SerializeField] private GameObject UI;

    /// <summary>フェードインにかかる時間（秒）</summary>
    [SerializeField] private float fadeInDuration = 1.5f;

    /// <summary>フェードイン完了後の待機時間（秒）</summary>
    [SerializeField] private float waitTime = 2f;

    /// <summary>透明度制御用のCanvasGroup</summary>
    private CanvasGroup canvasGroup;

    /// <summary>
    /// 初期化処理。CanvasGroupの設定とUIの非表示化を行います。
    /// </summary>
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // CanvasGroupがなければ追加
        if (canvasGroup == null)
            canvasGroup = UI.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        UI.SetActive(false);
    }

    /// <summary>
    /// 毎フレームの更新処理（現在は空実装）。
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// プレイヤーがトリガー領域に入った時の処理。
    /// フェードインを開始し、エンディングシーンへ遷移します。
    /// </summary>
    /// <param name="other">トリガーに接触したコライダー</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(true);
            StartCoroutine(FadeInAndLoadScene());
            // 重複発動を防ぐためコライダーを無効化
            GetComponent<Collider2D>().enabled = false;
        }
    }

    /// <summary>
    /// UIをフェードインさせてからエンディングシーンをロードするコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator FadeInAndLoadScene()
    {
        UI.SetActive(true);
        float timer = 0f;
        // 透明度を徐々に上げる
        while (timer < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeInDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 待機後、エンディングシーンへ遷移
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("End");
    }
}

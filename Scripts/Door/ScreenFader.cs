using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 画面のフェードイン/フェードアウト効果を管理するシングルトンクラス。
/// シーン遷移やテレポート時の演出に使用されます。
/// </summary>
public class ScreenFader : MonoBehaviour
{
    /// <summary>シングルトンインスタンス</summary>
    static ScreenFader _instance;

    /// <summary>透明度を制御するCanvasGroup</summary>
    CanvasGroup _group;

    /// <summary>
    /// シングルトンインスタンスを取得します。
    /// 存在しない場合は自動的に作成されます。
    /// </summary>
    public static ScreenFader Instance
    {
        get
        {
            if (_instance) return _instance;

            // 新しいGameObjectを作成
            var go = new GameObject("ScreenFader");
            DontDestroyOnLoad(go);

            // Canvasの設定
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 9999; // 最前面に表示

            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();

            // 黒い画面オーバーレイを作成
            var imgGO = new GameObject("Black");
            imgGO.transform.SetParent(go.transform, false);
            var img = imgGO.AddComponent<Image>();
            img.color = Color.black;
            img.raycastTarget = false;

            // 画面全体に広げる
            var rt = img.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;

            // CanvasGroupで透明度を制御
            _instance = go.AddComponent<ScreenFader>();
            _instance._group = go.AddComponent<CanvasGroup>();
            _instance._group.alpha = 0f; // 初期状態は透明
            return _instance;
        }
    }

    /// <summary>
    /// 画面をフェードアウト（暗くする）します。
    /// </summary>
    /// <param name="duration">フェードにかかる時間（秒）</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    public IEnumerator FadeOut(float duration = 0.25f) => FadeTo(1f, duration);

    /// <summary>
    /// 画面をフェードイン（明るくする）します。
    /// </summary>
    /// <param name="duration">フェードにかかる時間（秒）</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    public IEnumerator FadeIn(float duration = 0.25f) => FadeTo(0f, duration);

    /// <summary>
    /// 指定した透明度までフェードするコルーチン。
    /// </summary>
    /// <param name="target">目標の透明度（0=透明、1=不透明）</param>
    /// <param name="duration">フェードにかかる時間（秒）</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    IEnumerator FadeTo(float target, float duration)
    {
        float start = _group.alpha;
        float t = 0f;
        // 時間経過に応じて透明度を補間
        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // Time.timeScaleの影響を受けない
            _group.alpha = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }
        _group.alpha = target;
    }
}

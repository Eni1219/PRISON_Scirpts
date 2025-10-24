using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    static ScreenFader _instance;
    CanvasGroup _group;

    public static ScreenFader Instance
    {
        get
        {
            if (_instance) return _instance;
            var go = new GameObject("ScreenFader");
            DontDestroyOnLoad(go);

            // Canvas
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 9999;

            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();

            // 黑幕
            var imgGO = new GameObject("Black");
            imgGO.transform.SetParent(go.transform, false);
            var img = imgGO.AddComponent<Image>();
            img.color = Color.black;
            img.raycastTarget = false;

            var rt = img.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;

            // CanvasGroup 控制透明度
            _instance = go.AddComponent<ScreenFader>();
            _instance._group = go.AddComponent<CanvasGroup>();
            _instance._group.alpha = 0f; // 初始透明
            return _instance;
        }
    }

    public IEnumerator FadeOut(float duration = 0.25f) => FadeTo(1f, duration);
    public IEnumerator FadeIn(float duration = 0.25f) => FadeTo(0f, duration);

    IEnumerator FadeTo(float target, float duration)
    {
        float start = _group.alpha;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // 不受 Time.timeScale 影响
            _group.alpha = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }
        _group.alpha = target;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 破壊可能な隠し壁を管理するクラス。
/// プレイヤーの攻撃で破壊でき、IBreakableインターフェースを実装しています。
/// </summary>
public class HiddenWall : MonoBehaviour, IBreakable
{
    /// <summary>壁の耐久値（攻撃を受けると減少）</summary>
    [SerializeField] private int hp = 3;

    /// <summary>壁のコライダー（衝突判定用）</summary>
    [SerializeField] private Collider2D col;

    /// <summary>壁のタイルマップ（フラッシュエフェクト用）</summary>
    [SerializeField] private Tilemap tilemap;

    /// <summary>効果音再生用のAudioSource</summary>
    [Header("SE")]
    [SerializeField] private AudioSource audioSource;

    /// <summary>ヒット時の効果音</summary>
    [SerializeField] private AudioClip hitSE;

    /// <summary>破壊時の効果音</summary>
    [SerializeField] private AudioClip breakSE;

    /// <summary>
    /// コンポーネントの初期化と参照の取得を行います。
    /// </summary>
    private void Awake()
    {
        // コライダーの自動取得
        if (!col)
            col = GetComponent<Collider2D>();
        // タイルマップの自動取得
        if (!tilemap)
            tilemap = GetComponent<Tilemap>();
        // AudioSourceの自動取得または追加
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    /// <summary>
    /// ダメージを受けた時の処理。HPが0以下になると破壊されます。
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    /// <param name="hitDir">攻撃の方向ベクトル（現在未使用）</param>
    public void TakeHit(int damage, Vector2 hitDir)
    {
        // 最低1ダメージは与える
        hp -= Mathf.Max(1, damage);
        if (hp <= 0)
            Break();
        else
            HitFeedBack();
    }

    /// <summary>
    /// 壁を破壊します。効果音を再生後、オブジェクトを削除します。
    /// </summary>
    private void Break()
    {
        if (breakSE) audioSource.PlayOneShot(breakSE);
        // 効果音の長さ分待ってから削除（なければ即時）
        Destroy(gameObject, breakSE ? breakSE.length : .01f);
    }

    /// <summary>
    /// 攻撃を受けた時のフィードバック処理（シェイク・フラッシュ・SE）。
    /// </summary>
    private void HitFeedBack()
    {
        StartCoroutine(ShakeRoutine(.3f, .1f));
        StartCoroutine(FlashRoutine());
        if (hitSE) audioSource.PlayOneShot(hitSE);
    }

    /// <summary>
    /// オブジェクトを揺らすコルーチン。
    /// </summary>
    /// <param name="duration">揺れの持続時間（秒）</param>
    /// <param name="magnitude">揺れの強さ</param>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        // 指定時間ランダムに位置をずらす
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // 元の位置に戻す
        transform.localPosition = originalPos;
    }

    /// <summary>
    /// タイルマップを白く点滅させるコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator FlashRoutine()
    {
        if (tilemap == null) yield break;
        Color original = tilemap.color;

        // 白く点滅
        tilemap.color = Color.white;
        yield return new WaitForSeconds(0.3f);

        // 元の色に戻す
        tilemap.color = original;
    }
}

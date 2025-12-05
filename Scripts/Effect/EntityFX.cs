using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エンティティ（プレイヤーや敵）のビジュアルエフェクトを管理するクラス。
/// ダメージを受けた時のフラッシュやダッシュクールダウン表示などを制御します。
/// </summary>
public class EntityFX : MonoBehaviour
{
    /// <summary>対象のSpriteRenderer</summary>
    private SpriteRenderer sr;

    /// <summary>ヒット時に使用するマテリアル</summary>
    [Header("Flash Fix")]
    [SerializeField] private Material hitMat;

    /// <summary>ダッシュクールダウン時に使用するマテリアル</summary>
    [SerializeField] private Material dashCDMat;

    /// <summary>元のマテリアル（保存用）</summary>
    private Material originalMat;

    /// <summary>
    /// 初期化処理。SpriteRendererと元のマテリアルを取得します。
    /// </summary>
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    /// <summary>
    /// ヒット時のフラッシュエフェクトを再生するコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originalMat;
    }

    /// <summary>
    /// ダッシュクールダウン時のエフェクトを再生するコルーチン。
    /// </summary>
    /// <returns>コルーチン用のIEnumerator</returns>
    private IEnumerator DashCoolDownFX()
    {
        sr.material = dashCDMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originalMat;
    }

    /// <summary>
    /// 赤色の点滅エフェクトを切り替えます。
    /// InvokeRepeatingで呼び出して使用します。
    /// </summary>
    private void RedColorBlink()
    {
        // 現在白なら赤に、それ以外なら白に切り替え
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    /// <summary>
    /// 赤色点滅エフェクトをキャンセルし、元の色に戻します。
    /// </summary>
    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}

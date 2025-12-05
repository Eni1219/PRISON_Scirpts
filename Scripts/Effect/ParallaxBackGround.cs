using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パララックス（視差）効果を持つ背景を制御するクラス。
/// カメラの移動に応じて背景をずらすことで奥行き感を演出します。
/// </summary>
public class ParallaxBackGround : MonoBehaviour
{
    /// <summary>追従するカメラ</summary>
    [SerializeField] private Camera cam;

    /// <summary>パララックス効果の強さ（0=動かない、1=カメラと同速）</summary>
    [SerializeField] private float parallaxEffect;

    /// <summary>背景の現在のX位置</summary>
    private float xPosition;

    /// <summary>背景スプライトの幅</summary>
    private float length;

    /// <summary>
    /// 初期化処理。背景の幅と初期位置を取得します。
    /// </summary>
    void Start()
    {
        // 子のSpriteRendererから幅を取得
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    /// <summary>
    /// LateUpdate処理。カメラ移動後にパララックス効果を適用します。
    /// </summary>
    void LateUpdate()
    {
        // カメラ移動量を計算（パララックス効果適用）
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        // 背景位置を更新
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        // 無限ループのため、背景がカメラ範囲外に出たら位置をリセット
        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 破壊可能なオブジェクトが実装するインターフェース。
/// 攻撃を受けた時のダメージ処理を定義します。
/// </summary>
public interface IBreakable
{
    /// <summary>
    /// ダメージを受けた時に呼び出されるメソッド。
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    /// <param name="hirDir">攻撃の方向ベクトル</param>
    void TakeHit(int damage, Vector2 hirDir);
}

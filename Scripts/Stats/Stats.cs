using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステータス値を管理するシリアライズ可能なクラス。
/// 基本値と修正値のリストを持ち、最終的な値を計算します。
/// </summary>
[System.Serializable]
public class Stats
{
    /// <summary>ステータスの基本値</summary>
    [SerializeField] private int baseValue;

    /// <summary>ステータスに適用される修正値のリスト</summary>
    public List<int> modifiers = new List<int>();

    /// <summary>
    /// 基本値と全ての修正値を合計した最終的なステータス値を取得します。
    /// </summary>
    /// <returns>計算後のステータス値</returns>
    public int GetValue()
    {
        int finalValue = baseValue;
        // 全ての修正値を加算
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    /// <summary>
    /// 修正値を追加します。
    /// </summary>
    /// <param name="_modifier">追加する修正値</param>
    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    /// <summary>
    /// 修正値を削除します。
    /// </summary>
    /// <param name="_modifier">削除する修正値</param>
    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
    }
}

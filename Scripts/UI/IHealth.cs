using System;

/// <summary>
/// ヘルス（HP）を持つオブジェクトが実装するインターフェース。
/// HPバーUIとの連携に使用します。
/// </summary>
public interface IHealth
{
    /// <summary>最大HP値</summary>
    int maxHealth { get; }

    /// <summary>現在HP値</summary>
    int currentHealth { get; }

    /// <summary>HP変更時に発火するイベント（現在HP, 最大HP）</summary>
    event Action<int, int> OnHealthChanged;

    /// <summary>死亡時に発火するイベント</summary>
    event Action OnDied;
}

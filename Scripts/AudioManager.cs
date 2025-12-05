using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドデータを保持するシリアライズ可能なクラス。
/// Unityインスペクター上でBGMやSEの設定を行うために使用します。
/// </summary>
[Serializable]
public class Sound
{
    /// <summary>サウンドの識別名（再生・停止時に使用）</summary>
    public string name;

    /// <summary>再生する音声クリップ</summary>
    public AudioClip clip;

    /// <summary>音量（0.0〜1.0）</summary>
    [Range(0f, 1f)] public float volume = 1f;

    /// <summary>ピッチ（0.1〜3.0）</summary>
    [Range(.1f, 3f)] public float pitch = 1f;

    /// <summary>ループ再生を行うかどうか</summary>
    public bool loop = false;

    /// <summary>実際に再生するAudioSourceコンポーネント（内部使用）</summary>
    [HideInInspector] public AudioSource source;
}

/// <summary>
/// ゲーム全体のオーディオを管理するシングルトンクラス。
/// BGMやSEの再生・停止を一元管理し、シーンを跨いでも保持されます。
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>シングルトンインスタンス（どこからでもアクセス可能）</summary>
    public static AudioManager instance;

    /// <summary>管理対象のサウンドリスト（インスペクターで設定）</summary>
    public List<Sound> sounds;

    /// <summary>
    /// Awake時にシングルトンの初期化と各サウンドのAudioSource設定を行います。
    /// </summary>
    private void Awake()
    {
        #region Singleton Pattern
        // シングルトンの初期化（重複インスタンスは破棄）
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // シーン遷移時も破棄されないように設定
        DontDestroyOnLoad(gameObject);
        #endregion

        // 各サウンドに AudioSource コンポーネントを追加し、設定を反映
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// 指定した名前のサウンドを再生します。
    /// </summary>
    /// <param name="name">再生するサウンドの識別名</param>
    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    /// <summary>
    /// 指定した名前のサウンドを停止します。
    /// </summary>
    /// <param name="name">停止するサウンドの識別名</param>
    public void Stop(string name)
    {
        Sound s = sounds.Find(sounds => sounds.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    /// <summary>
    /// 指定した名前のサウンドを一度だけ再生します（重複再生可能）。
    /// 同じSEを連続で鳴らしたい場合に使用します。
    /// </summary>
    /// <param name="name">再生するサウンドの識別名</param>
    public void PlayOneShot(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;
        s.source.PlayOneShot(s.clip, s.volume);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// サウンドデータを保持するクラス（インスペクターで設定可能）
[Serializable]
public class Sound
{
    public string name; // サウンド名（識別用）
    public AudioClip clip; // 再生する音声クリップ
    [Range(0f, 1f)] public float volume = 1f; // 音量
    [Range(.1f, 3f)] public float pitch = 1f; // ピッチ
    public bool loop = false; // ループ再生するかどうか
    [HideInInspector] public AudioSource source; // 実際に再生する AudioSource（内部用）
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // シングルトンインスタンス
    public List<Sound> sounds; // サウンドリスト（インスペクターで設定）

    private void Awake()
    {
        #region Singleton Pattern
        // シングルトンの初期化（重複防止）
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); // シーンを跨いでも破棄されない
        #endregion

        // 各サウンドに AudioSource を追加して設定
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // サウンド名で再生
    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    // サウンド名で停止
    public void Stop(string name)
    {
        Sound s = sounds.Find(sounds => sounds.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    // 一度だけ再生（重ねて鳴らす用）
    public void PlayOneShot(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;
        s.source.PlayOneShot(s.clip, s.volume);
    }
}

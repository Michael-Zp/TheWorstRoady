using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public int AudioSourcesPoolSize = 100;

    private int _audioSourcesUsedCount = 0;
    private Stack<AudioSource> _audioSourcePool = new Stack<AudioSource>();
    private Dictionary<string, AudioSource> _usedAudioSources = new Dictionary<string, AudioSource>();


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        var audioPoolGameObject = Instantiate(new GameObject(), transform);

        for (int i = 0; i < AudioSourcesPoolSize; i++)
        {
            AudioSource source = audioPoolGameObject.AddComponent<AudioSource>();
            _audioSourcePool.Push(source);
        }
    }
    
    /// <summary>
    /// Plays the clip and returns a string, that functions as a key for the AudioSource
    /// </summary>
    /// <returns></returns>
    public string PlayAudioClip(AudioClip clip, bool loop = false)
    {
        var key = AddAudioClip(clip);

        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            source.loop = loop;
            source.Play();
        }

        return key;
    }

    public void PlayAudioClip(string key)
    {
        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            source.Play();
        }
    }

    public string AddAudioClip(AudioClip clip)
    {
        AudioSource source;

        if (_audioSourcePool.Count > 0)
        {
            source = _audioSourcePool.Pop();
        }
        else
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.clip = clip;

        _audioSourcesUsedCount++;

        var key = _audioSourcesUsedCount + "";

        _usedAudioSources.Add(key, source);

        return key;
    }

    public void PauseAudioClip(string key)
    {
        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            source.Pause();
        }
    }

    public void UnPauseAudioClip(string key)
    {
        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            source.UnPause();
        }
    }

    public void StopAudioClip(string key)
    {
        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            source.Stop();
        }
    }

    public void SetVolume(string key, float volume)
    {
        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            source.volume = volume;
        }
    }

    public bool IsPlaying(string key)
    {
        bool isPlaying = false;
        AudioSource source;
        _usedAudioSources.TryGetValue(key, out source);

        if (source != null)
        {
            isPlaying = source.isPlaying;
        }

        return isPlaying;
    }

    public void FreeAudioSource(string key)
    {
        if (_usedAudioSources.ContainsKey(key))
        {
            AudioSource source;
            _usedAudioSources.TryGetValue(key, out source);

            if (source != null)
            {
                _usedAudioSources.Remove(key);
                _audioSourcePool.Push(source);
            }

        }
    }

}

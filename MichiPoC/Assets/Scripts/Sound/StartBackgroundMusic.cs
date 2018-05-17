using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackgroundMusic : MonoBehaviour {

    public AudioClip BackgroundClip;

    private string _clipName;

    private void Start()
    {
        string _clipName = AudioManager.Instance.PlayAudioClip(BackgroundClip, true);
    }

    private void OnDestroy()
    {
        AudioManager.Instance.StopAudioClip(_clipName);
    }
}

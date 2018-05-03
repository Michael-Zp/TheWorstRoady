using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    //Delegate Types
    public delegate void VoidVoidHandler();
    public delegate void VoidIntHandler(int arg);
    public delegate void VoidStringHandler(string arg);
    public delegate void VoidVector3Handler(Vector3 arg);

    //Events
    public event VoidVoidHandler GuitarOfPlayerDestroyedEvent;
    public void DestroyGuitarOfPlayer() { if (GuitarOfPlayerDestroyedEvent != null) GuitarOfPlayerDestroyedEvent(); }

    public event VoidVector3Handler StunPlayerEvent;
    public void StunPlayer(Vector3 pucherPosition) { if (StunPlayerEvent != null) StunPlayerEvent(pucherPosition); }

    public event VoidVector3Handler PunchGuitarOutOfHandsEvent;
    public void PunchGuitarOutOfHands(Vector3 pucherPosition) { if (PunchGuitarOutOfHandsEvent != null) PunchGuitarOutOfHandsEvent(pucherPosition); }

    public event VoidVoidHandler GameWonEvent;
    public void GameWon() { if (GameWonEvent != null) GameWonEvent(); }

    public event VoidStringHandler GameOverEvent;
    public void GameOver(string reason) { if (GameOverEvent != null) GameOverEvent(reason); }
    
    public event VoidIntHandler AddScoreEvent;
    public void AddScore(int score) { if (AddScoreEvent != null) AddScoreEvent(score); }

    public event VoidIntHandler ShowGameWonScreenEvent;
    public void ShowGameWonScreen(int score) { if (ShowGameWonScreenEvent != null) ShowGameWonScreenEvent(score); }

    public event VoidStringHandler ShowGameOverScreenEvent;
    public void ShowGameOverScreen(string arg) { if (ShowGameOverScreenEvent != null) ShowGameOverScreenEvent(arg); }

    public event VoidIntHandler ShowScoreEvent;
    public void ShowScore(int score) { if (ShowScoreEvent != null) ShowScoreEvent(score); }

    public event VoidIntHandler UnlockLevelEvent;
    public void UnlockLevel(int level) { if (UnlockLevelEvent != null) UnlockLevelEvent(level); }

    //Singleton
    private static EventSystem _instance = null;
    public static EventSystem Instance {
        get {
            return _instance;
        }
    }

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
    }
}

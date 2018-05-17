using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    //Delegate Types
    public delegate void VoidVoidHandler();
    public delegate void VoidIntHandler(int arg);
    public delegate void VoidIntIntHandler(int arg1, int arg2);
    public delegate void VoidFloatHandler(float arg1);
    public delegate void VoidFloatBoolHandler(float arg1, bool arg2);
    public delegate void VoidStringHandler(string arg);
    public delegate void VoidVector3Handler(Vector3 arg);
    public delegate int IntVoidHandler();
    public delegate List<Vector4> ListVector4VoidHandler();

    //Events
    public event VoidVoidHandler GuitarOfPlayerDestroyedEvent;
    public void DestroyGuitarOfPlayer() { if (GuitarOfPlayerDestroyedEvent != null) GuitarOfPlayerDestroyedEvent(); }

    public event VoidVector3Handler StunPlayerEvent;
    public void StunPlayer(Vector3 pucherPosition) { if (StunPlayerEvent != null) StunPlayerEvent(pucherPosition); }

    public event VoidVector3Handler PunchGuitarOutOfHandsEvent;
    public void PunchGuitarOutOfHands(Vector3 pucherPosition) { if (PunchGuitarOutOfHandsEvent != null) PunchGuitarOutOfHandsEvent(pucherPosition); }




    public event VoidVoidHandler GameWonEvent;
    public void GameWon() { if (GameWonEvent != null) GameWonEvent(); }

    public event VoidVoidHandler GameOverEvent;
    public void GameOver() { if (GameOverEvent != null) GameOverEvent(); }

    public event VoidVoidHandler ShowGameWonScreenEvent;
    public void ShowGameWonScreen() { if (ShowGameWonScreenEvent != null) ShowGameWonScreenEvent(); }

    public event VoidVoidHandler ShowGameOverScreenEvent;
    public void ShowGameOverScreen() { if (ShowGameOverScreenEvent != null) ShowGameOverScreenEvent(); }

    public event VoidIntHandler UnlockLevelEvent;
    public void UnlockLevel(int level) { if (UnlockLevelEvent != null) UnlockLevelEvent(level); }

    public event VoidVoidHandler LoadNextLevelEvent;
    public void LoadNextLevel() { if (LoadNextLevelEvent != null) LoadNextLevelEvent(); }



    public event VoidIntHandler ScareOfFanBaseEvent;
    public void ScareOfFanBase(int numberOfFans) { if (ScareOfFanBaseEvent != null) ScareOfFanBaseEvent(numberOfFans); }

    public event VoidIntHandler GetCloserToBeFiredEvent;
    public void GetCloserToBeFired(int percentageToIncrease) { if (GetCloserToBeFiredEvent != null) GetCloserToBeFiredEvent(percentageToIncrease); }

    public event VoidIntIntHandler ScareOfFanBaseAndGetCloserToBeFiredEvent;
    public void ScareOfFanBaseAndGetCloserToBeFired(int numberOfFans, int percentageToIncrease) { if (ScareOfFanBaseEvent != null && GetCloserToBeFiredEvent != null) { ScareOfFanBase(numberOfFans); GetCloserToBeFired(percentageToIncrease); } }

    public event VoidFloatBoolHandler UpdateScareOfFanBaseUIEvent;
    public void UpdateScareOfFanBaseUI(float arg1, bool arg2) { if (UpdateScareOfFanBaseUIEvent != null) UpdateScareOfFanBaseUIEvent(arg1, arg2); }

    public event VoidFloatBoolHandler UpdateGetCloserToBeUIFiredEvent;
    public void UpdateGetCloserToBeFiredUI(float arg1, bool arg2) { if (UpdateGetCloserToBeUIFiredEvent != null) UpdateGetCloserToBeUIFiredEvent(arg1, arg2); }

    public event VoidFloatHandler SetScareOfFanBaseUIEvent;
    public void SetScareOfFanBaseUI(float arg1) { if (SetScareOfFanBaseUIEvent != null) SetScareOfFanBaseUIEvent(arg1); }

    public event VoidFloatHandler SetGetCloserToBeUIFiredEvent;
    public void SetGetCloserToBeFiredUI(float arg1) { if (SetGetCloserToBeUIFiredEvent != null) SetGetCloserToBeUIFiredEvent(arg1); }



    public event IntVoidHandler GetHowManyEnemiesAreInLevelEvent;
    public int GetHowManyEnemiesAreInLevel() { if (GetHowManyEnemiesAreInLevelEvent != null) return GetHowManyEnemiesAreInLevelEvent(); return 0; }

    public event IntVoidHandler GetMaxEnemiesInLevelEvent;
    public int GetMaxEnemiesInLevel() { if (GetMaxEnemiesInLevelEvent != null) return GetMaxEnemiesInLevelEvent(); return 0; }

    public event VoidVoidHandler IncrementEnemiesInLevelEvent;
    public void IncrementEnemiesInLevel() { if (IncrementEnemiesInLevelEvent != null) IncrementEnemiesInLevelEvent(); }

    public event VoidVoidHandler DecrementEnemiesInLevelEvent;
    public void DecrementEnemiesInLevel() { if (DecrementEnemiesInLevelEvent != null) DecrementEnemiesInLevelEvent(); }



    public event VoidVector3Handler AddBloodSplatterEvent;
    public void AddBloodSplatter(Vector3 arg) { if (AddBloodSplatterEvent != null) AddBloodSplatterEvent(arg); }

    public event ListVector4VoidHandler GetBloodSplattersEvent;
    public List<Vector4> GetBloodSplatters() { if (GetBloodSplattersEvent != null) return GetBloodSplattersEvent(); return new List<Vector4>() { new Vector4(-100, -100, -100, -100) }; }

    public event ListVector4VoidHandler GetBloodSplatterPatternsEvent;
    public List<Vector4> GetBloodSplatterPatterns() { if (GetBloodSplatterPatternsEvent != null) return GetBloodSplatterPatternsEvent(); return new List<Vector4>() { new Vector4(-100, -100, -100, -100) }; }

    public event IntVoidHandler GetBloodSplatterCountEvent;
    public int GetBloodSplatterCount() { if (GetBloodSplatterCountEvent != null) return GetBloodSplatterCountEvent(); return 0; }


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

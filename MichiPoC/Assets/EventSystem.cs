using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    //Delegate Types
    public delegate void VoidVoidHandler();
    public delegate void VoidVector3Handler(Vector3 arg);

    //Events
    public event VoidVoidHandler GuitarOfPlayerDestroyedEvent;
    public void DestroyGuitarOfPlayer() { if (GuitarOfPlayerDestroyedEvent != null) GuitarOfPlayerDestroyedEvent(); }

    public event VoidVector3Handler StunPlayerEvent;
    public void StunPlayer(Vector3 pucherPosition) { if (StunPlayerEvent != null) StunPlayerEvent(pucherPosition); }

    public event VoidVector3Handler PunchGuitarOutOfHandsEvent;
    public void PunchGuitarOutOfHands(Vector3 pucherPosition) { if (PunchGuitarOutOfHandsEvent != null) PunchGuitarOutOfHandsEvent(pucherPosition); }



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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{



    //First sample event trigger.
    public delegate void SampleEventHandler(string payload);
    public static event SampleEventHandler sampleEvent;
    public void triggerSampleEvent(string text) { sampleEvent(text); }





    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

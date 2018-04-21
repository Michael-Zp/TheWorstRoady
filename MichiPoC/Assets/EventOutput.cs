using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOutput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventSystem.sampleEvent += displayText;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void displayText(string text)
    {
        Debug.Log(text);
    }
}

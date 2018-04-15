using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    public GameObject IntroText1;
    public GameObject IntroText2;
    public GameObject IntroUi;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            if (IntroText1.activeInHierarchy)
            {
                IntroText1.SetActive(false);
                IntroText2.SetActive(true);
            }
            else if(IntroText2.activeInHierarchy)
            {
                IntroText2.SetActive(false);
                IntroUi.SetActive(false);
                Time.timeScale = 1.0f;
            }

        }
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text ScoreText;
    public Text GameOverText;
    public Text GameWonText;

    private int Score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int score)
    {
        Score += score;
        ScoreText.text = "Score: " + Score;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        Debug.Log("GameOver");
        GameOverText.gameObject.SetActive(true);
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f;
        Debug.Log("Won");
        GameWonText.text += Score;
        GameWonText.gameObject.SetActive(true);
    }
}

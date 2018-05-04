using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameOver;
    public Text GameOverReasonText;
    public GameObject GameWon;
    public Text GameWonScoreText;

    public List<GameObject> ScoreHeads = new List<GameObject>();

    private void Awake()
    {
        EventSystem.Instance.ShowGameWonScreenEvent += ShowGameWonScreen;
        EventSystem.Instance.ShowGameOverScreenEvent += ShowGameOverScreen;
        EventSystem.Instance.ShowScoreEvent += ShowScore;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.ShowGameWonScreenEvent -= ShowGameWonScreen;
        EventSystem.Instance.ShowGameOverScreenEvent -= ShowGameOverScreen;
        EventSystem.Instance.ShowScoreEvent -= ShowScore;
    }

    private void ShowScore(int score)
    {
        for(int i = 0; i < score && i < ScoreHeads.Count; i++)
        {
            ScoreHeads[ScoreHeads.Count - 1 - i].SetActive(true);
        }
    }

    private void ShowGameOverScreen(string reason)
    {
        GameOverReasonText.text = reason;
        GameOver.gameObject.SetActive(true);
    }

    private void ShowGameWonScreen(int score)
    {
        string[] scoreText = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

        score = score > 10 ? 10 : score;

        GameWonScoreText.text = "Score: " + scoreText[score] + "\n\n";
        GameWon.gameObject.SetActive(true);
    }
}

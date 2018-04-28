using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public GameObject GameOver;
    public Text GameOverReasonText;
    public GameObject GameWon;
    public Text GameWonScoreText;

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
        ScoreText.text = "Score: " + score;
    }

    private void ShowGameOverScreen(string reason)
    {
        GameOverReasonText.text = reason;
        GameOver.gameObject.SetActive(true);
    }

    private void ShowGameWonScreen(int score)
    {
        GameWonScoreText.text = "Score: " + score;
        GameWon.gameObject.SetActive(true);
    }
}

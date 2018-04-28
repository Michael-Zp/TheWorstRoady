using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text GameOverText;
    public Text GameWonText;

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

    private void ShowGameOverScreen()
    {
        GameOverText.gameObject.SetActive(true);
    }

    private void ShowGameWonScreen(int score)
    {
        GameWonText.text += score;
        GameWonText.gameObject.SetActive(true);
    }
}

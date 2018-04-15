using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    public Text ScoreText;
    public Text GameOverText;
    public Text GameWonText;

    private int Score;

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

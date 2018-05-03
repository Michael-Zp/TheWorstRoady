using UnityEngine;
using UnityEngine.UI;

public class HighscoresManager : MonoBehaviour
{
    public Text[] HighscoreEntries;

    private void Start()
    {
        HighscoresData.HighscoresCount = HighscoreEntries.Length;

        UpdateHighscores();
    }

    private void OnEnable()
    {
        UpdateHighscores();
    }

    private void UpdateHighscores()
    {
        for (int i = 0; i < HighscoresData.HighscoresCount; i++)
        {
            if (i > HighscoresData.CurrentHighscoresCount() - 1)
            {
                continue;
            }
            else
            {
                HighscoreEntries[i].text = HighscoresData.GetHighscore(i) + "";
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class HighscoresData : MonoBehaviour {

    private static List<float> Highscores = new List<float>();

    public static int HighscoresCount = 4;


    public static void AddHighscore(float score)
    {
        Highscores.Add(score);

        Highscores.Sort((x, y) => y.CompareTo(x));

        if (Highscores.Count > HighscoresCount)
        {
            Highscores.RemoveRange(HighscoresCount, Highscores.Count - HighscoresCount);
        }
    }

    public static int CurrentHighscoresCount()
    {
        return Highscores.Count;
    }

    public static float GetHighscore(int index)
    {
        return Highscores[index];
    }

}

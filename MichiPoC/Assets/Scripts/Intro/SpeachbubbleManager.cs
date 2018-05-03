using UnityEngine;

public class SpeachbubbleManager : MonoBehaviour
{
    public TextMesh Text;
    public GameObject Speachbubble;

    public void Speak(string text)
    {
        Text.text = text;
        Speachbubble.SetActive(true);
    }

    public void FinishSpeaking()
    {
        Text.text = "";
        Speachbubble.SetActive(false);
    }
}

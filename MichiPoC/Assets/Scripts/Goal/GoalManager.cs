using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public SpriteRenderer SpeachBubble;
    public SpriteRenderer TimeSprite;
    public SpriteRenderer FullStopwatch;
    public SpriteRenderer OnlyEdge;
    public int MaxTime = 60;
    public ItemType WantedType;

    private Color _defaultColor;
    private float _startTime;

    void Awake()
    {
        _defaultColor = SpeachBubble.color;
        _startTime = Time.time;
    }

    void LateUpdate()
    {
        UpdateTime();
    }

    public bool GiveItem(ItemType type)
    {
        if (WantedType == type)
        {
            EventSystem.Instance.GameWon();
            return true;
        }
        else
        {
            EventSystem.Instance.GameOver("You idiot brought the wrong guitar. Get out!");
            return false;
        }
    }

    private void UpdateTime()
    {
        float currentTime = Time.time - _startTime;

        float timeRatio = currentTime / MaxTime;

        TimeSprite.material.SetFloat("_TimeRatio", timeRatio);

        float startBlinkingTime = _startTime + 0.8f * MaxTime;

        if (Time.time > startBlinkingTime)
        {
            float blinkFactor = Time.time - startBlinkingTime;

            Color startColorOfStopwatchOrEdge = new Color(1, 1, 1, 1);
            Color startColorOfTime = new Color(0, 0, 0, 1);
            Color signalColor = new Color(1, 0, 0, 1);

            float ratio = Mathf.Abs(Mathf.Sin(blinkFactor * 5));

            FullStopwatch.color = Color.Lerp(startColorOfStopwatchOrEdge, signalColor, ratio);
            OnlyEdge.color = Color.Lerp(startColorOfStopwatchOrEdge, signalColor, ratio);

            Color weakSignalColor = new Color(0.3f, 0, 0, 1);
            TimeSprite.material.SetColor("_Color", Color.Lerp(startColorOfTime, weakSignalColor, ratio));
        }

        if (currentTime > MaxTime)
        {
            EventSystem.Instance.GameOver("To late. You are fired!");
            Destroy(GetComponent<GoalManager>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeachBubble.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeachBubble.color = _defaultColor;
        }
    }
}

using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public SpriteRenderer SpeachBubble;
    public TextMesh TimeText;
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

        TimeText.text = (int)Mathf.Clamp(Mathf.Floor(MaxTime - currentTime), 0, MaxTime) + "";

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

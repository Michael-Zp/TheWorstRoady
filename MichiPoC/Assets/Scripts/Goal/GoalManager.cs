using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public SpriteRenderer SpeachBubble;
    public TextMesh TimeText;
    public int MaxTime = 60;
    public ItemType WantedType;

    private Color _defaultColor;

    void Start()
    {
        _defaultColor = SpeachBubble.color;
    }

    void Update()
    {
        UpdateTime();
    }

    public bool GiveItem(ItemType type)
    {
        if (WantedType == type)
        {
            GameManager.Instance.WonGame();
            return true;
        }
        else
        {
            GameManager.Instance.GameOver();
            return false;
        }
    }

    private void UpdateTime()
    {
        TimeText.text = (int)Mathf.Clamp(Mathf.Floor(MaxTime - Time.time), 0, MaxTime) + "";

        if (Time.time > MaxTime)
        {
            GameManager.Instance.GameOver();
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

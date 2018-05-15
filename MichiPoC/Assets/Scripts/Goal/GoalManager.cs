using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public SpriteRenderer SpeachBubble;
    public int RateToBeFiredPerSecond;
    public ItemType WantedType;

    private Color _defaultColor;
    private float _lastIncreaseOfFireRate;

    void Awake()
    {
        _defaultColor = SpeachBubble.color;
        _lastIncreaseOfFireRate = Time.time;
    }

    void LateUpdate()
    {
        if(Time.time - _lastIncreaseOfFireRate > 1.0f)
        {
            _lastIncreaseOfFireRate = Time.time;
            EventSystem.Instance.GetCloserToBeFired(RateToBeFiredPerSecond);
        }
    }

    public void GiveItem(ItemType type)
    {
        if (WantedType == type)
        {
            EventSystem.Instance.ScareOfFanBaseAndGetCloserToBeFired(-10, -40);
        }
        else
        {
            EventSystem.Instance.ScareOfFanBaseAndGetCloserToBeFired(40, 50);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemType type;
            if(other.gameObject.GetComponent<PlayerManager>().GetActiveItem(out type))
            {
                if(type == WantedType)
                {
                    SpeachBubble.color = Color.green;
                    return;
                }
            }

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

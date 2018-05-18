using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public SpriteRenderer SpeachBubble;
    public ItemType WantedType;

    private Color _defaultColor;

    void Awake()
    {
        _defaultColor = SpeachBubble.color;
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

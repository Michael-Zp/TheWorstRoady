using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public SpriteRenderer SpeachBubble;
    public GameObject Guitarr;

    public TextMesh TimeText;

    private bool _playerIsInRange = false;
    private Color _defaultColor;

    // Use this for initialization
    void Start()
    {
        _defaultColor = SpeachBubble.color;
    }

    // Update is called once per frame 
    void Update()
    {
        if (_playerIsInRange)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(Guitarr);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().WonGame();
            }
        }

        int maxTime = 60;

        TimeText.text = (int)Mathf.Clamp(Mathf.Floor(maxTime - Time.time), 0, maxTime) + "";

        if(Time.time > maxTime)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeachBubble.color = Color.red;
            _playerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeachBubble.color = _defaultColor;
            _playerIsInRange = false;
        }
    }
}

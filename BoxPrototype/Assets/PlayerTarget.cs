using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour {

    private GameObject Player;
    public GameObject AttackMark;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        AttackMark.SetActive(false);
	}
	


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.GetComponent<HitGroupy>().Groupies.Add(this.gameObject);
        AttackMark.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.GetComponent<HitGroupy>().Groupies.Remove(this.gameObject);
        AttackMark.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.GetComponent<HitGroupy>().CurrentHp = 0;
            Player.GetComponent<HitGroupy>().UpdateHp();
        }
    }
}


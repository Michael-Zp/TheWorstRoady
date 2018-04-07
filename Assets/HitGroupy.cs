using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGroupy : MonoBehaviour {

    public List<GameObject> Groupies = new List<GameObject>();
    public GameObject[] GuitarHp;
    public GameObject Guitar;

    private int CurrentHp;

	// Use this for initialization
	void Start () {
        CurrentHp = GuitarHp.Length;
	}
	
	// Update is called once per frame
	void Update () {

        if(Groupies.Count > 0)
        {
            GameObject nearestGroupy = Groupies[0];

            foreach (var groupy in Groupies)
            {
                if(Vector3.Distance(groupy.transform.position, this.transform.position) < Vector3.Distance(nearestGroupy.transform.position, this.transform.position))
                {
                    nearestGroupy = groupy;
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Groupies.Remove(nearestGroupy);
                Destroy(nearestGroupy);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddScore(1);
                CurrentHp--;
                UpdateHp();
            }
        }

	}

    private void UpdateHp()
    {
        GuitarHp[CurrentHp].SetActive(false);

        if(CurrentHp == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PickableManager>().Guitar.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PickableManager>().OriginalGuitar.SetActive(true);
            CurrentHp = GuitarHp.Length;

            for(int i = 0; i < CurrentHp; i++)
            {
                GuitarHp[i].SetActive(true);
            }
        }
    }
}

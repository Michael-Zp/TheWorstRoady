using System.Collections.Generic;
using UnityEngine;

public class UseActiveItem : MonoBehaviour
{
    private List<GameObject> _groupies = new List<GameObject>();
    private GoalManager _goal = null;
    
    void Update()
    {
        //If no action was performed this script should do nothing
        if (!Input.GetButtonDown("Action") || !GetComponent<PlayerInventory>().HasActiveItem())
        {
            return;
        }

        if (_goal != null)
        {
            _goal.GiveItem(GetComponent<PlayerInventory>().GetTypeOfActiveItem());
            GetComponent<PlayerInventory>().DestroyActiveItem();

        } 
        else if (_groupies.Count > 0)
        {
            bool foundHittableGroupy = false;

            GameObject nearestGroupy = _groupies[0];

            foreach (var groupy in _groupies)
            {
                if(groupy.GetComponent<EnemyManager>().Dead)
                {
                    continue;
                }
                else
                {
                    foundHittableGroupy = true;
                }

                if (Vector3.Distance(groupy.transform.position, transform.position) < Vector3.Distance(nearestGroupy.transform.position, transform.position))
                {
                    nearestGroupy = groupy;
                }
            }
            
            if(foundHittableGroupy)
            {
                nearestGroupy.GetComponent<EnemyManager>().Die();
                EventSystem.Instance.ScareOfFanBaseAndGetCloserToBeFired(10, 5);
                GetComponent<PlayerInventory>().UseActiveItem();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Groupy")
        {
            _groupies.Add(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            _goal = collision.gameObject.GetComponent<GoalManager>();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Groupy")
        {
            _groupies.Remove(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            _goal = null;
        }
    }
}

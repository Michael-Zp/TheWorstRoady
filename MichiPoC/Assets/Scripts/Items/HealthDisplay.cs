using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public List<GameObject> Health;

    public void DisplayHealth(int count)
    {
        for(int i = 0; i < Health.Count; i++)
        {
            Health[i].SetActive(false);
        }

        for(int i = 0; i < count && i < Health.Count; i++)
        {
            Health[i].SetActive(true);
        }
    }
}

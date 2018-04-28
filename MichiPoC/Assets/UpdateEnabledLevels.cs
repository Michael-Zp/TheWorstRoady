using System.Collections.Generic;
using UnityEngine;

public class UpdateEnabledLevels : MonoBehaviour
{
    public List<GameObject> Levels = new List<GameObject>();

    private void OnEnable()
    {
        for(int i = 0; i < GameManager.Instance.UnlockedLevels.Length; i++)
        {
            Levels[i].SetActive(GameManager.Instance.UnlockedLevels[i]);
        }
    }
}

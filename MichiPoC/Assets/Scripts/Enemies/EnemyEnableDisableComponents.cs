using System.Collections.Generic;
using UnityEngine;

public class EnemyEnableDisableComponents : MonoBehaviour
{
    public void DisableComponentsOnSpawn()
    {
        SetEnabledOnComponents(false);
    }

    public void EnableComponentsFromSpawn()
    {
        SetEnabledOnComponents(true);
    }

    private void SetEnabledOnComponents(bool state)
    {
        foreach (BoxCollider2D comp in GetComponents<BoxCollider2D>())
        {
            comp.enabled = state;
        }

        GetComponent<EnemyMovement>().enabled = state;
    }
}

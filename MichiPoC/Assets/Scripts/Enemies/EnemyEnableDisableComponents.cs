using UnityEngine;

public class EnemyEnableDisableComponents : MonoBehaviour
{
    public void RemoveComponentsOnDeath()
    {
        foreach (BoxCollider2D comp in GetComponents<BoxCollider2D>())
        {
            if (comp.isTrigger)
            {
                Destroy(comp);
            }
        }

        foreach(MonoBehaviour comp in GetComponents<MonoBehaviour>())
        {
            if(comp.GetType() != typeof(EnemyManager))
            {
                Destroy(comp);
            }
        }

        //Destroy(GetComponent<EnemyMovement>());
        //Destroy(GetComponent<AttackPlayer>());
        //Destroy(GetComponent<EnemyManager>());
        //Destroy(GetComponent<EnemyEnableDisableComponents>());
    }

    public void DisableComponentsOnSpawn()
    {
        SetEnabledOnSpawnComponents(false);
    }

    public void EnableComponentsFromSpawn()
    {
        SetEnabledOnSpawnComponents(true);
    }

    private void SetEnabledOnSpawnComponents(bool state)
    {
        foreach (BoxCollider2D comp in GetComponents<BoxCollider2D>())
        {
            comp.enabled = state;
        }

        GetComponent<EnemyMovement>().enabled = state;
    }
}

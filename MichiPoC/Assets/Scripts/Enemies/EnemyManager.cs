using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AttackType AttackType;

    public void Die()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
            playerManager.GroupyHitPlayer();
        }
    }
}


using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public AttackType AttackType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    private void Attack()
    {
        switch(AttackType)
        {
            case AttackType.DestroyGuitar:
                EventSystem.Instance.DestroyGuitarOfPlayer();
                break;

            case AttackType.StunPlayer:
                EventSystem.Instance.StunPlayer(transform.position);
                break;

            case AttackType.PunchGuitarOutOfHands:
                EventSystem.Instance.PunchGuitarOutOfHands(transform.position);
                break;
        }
    }
}


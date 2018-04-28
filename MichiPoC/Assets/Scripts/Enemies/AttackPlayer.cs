using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private AttackType _attackType;

    private void Start()
    {
        _attackType = GetComponent<EnemyManager>().AttackType;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    private void Attack()
    {
        switch(_attackType)
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


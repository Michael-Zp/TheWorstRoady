using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AttackType AttackType;
    public GameObject Blood;
    public bool Dead { get; set; }
    public EnemyEnableDisableComponents EnemyEnableDisableComponents;
    public Vector2 FlyAwayOnDeathForce;
    
    private Color _originalColor;

    private void Start()
    {
        _originalColor = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
            EventSystem.Instance.DecrementEnemiesInLevel();
        }
    }

    public void Die(Vector2 positionOfAttacker)
    {
        Dead = true;
        GetComponent<Renderer>().material.color = new Color(1, .7f, .7f, 0.5f);
        EventSystem.Instance.DecrementEnemiesInLevel();

        Blood.SetActive(true);

        EnemyEnableDisableComponents.RemoveComponentsOnDeath();

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        float direction = positionOfAttacker.x > transform.position.x ? -1 : 1;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(FlyAwayOnDeathForce.x * direction, FlyAwayOnDeathForce.y));
        GetComponent<Rigidbody2D>().AddTorque(100, ForceMode2D.Force);

        GetComponent<BoxCollider2D>().size /= 4;

        gameObject.layer = LayerMask.NameToLayer("DoNotCollideWithPlayerOrEnemies");
    }
}

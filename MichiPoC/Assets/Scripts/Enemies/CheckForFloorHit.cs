using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForFloorHit : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public EnemyEnableDisableComponents ReenableComponents;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Rigidbody.velocity.y <= 0 && collision.gameObject.tag == "Floor")
        {
            ReenableComponents.EnableComponentsFromSpawn();
            enabled = false;
        }
    }

}

using UnityEngine;

public class CheckForFloorHit : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public EnemyEnableDisableComponents ReenableComponents;

    private bool _isEnabled = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!_isEnabled && Rigidbody.velocity.y <= 0 && collision.gameObject.tag == "Floor")
        {
            ReenableComponents.EnableComponentsFromSpawn();
            gameObject.SetActive(false);
            _isEnabled = true;
        }
    }

}

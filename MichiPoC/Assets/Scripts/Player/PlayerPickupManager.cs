using UnityEngine;

public class PlayerPickupManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickable pickable = collision.gameObject.GetComponent<Pickable>();

        if (pickable != null)
        {
            var itemWasPickedUp = GetComponent<PlayerInventory>().PickUpItem(pickable.Type, collision.gameObject.transform.position, collision.gameObject.transform.rotation, pickable.IsActive, pickable.MaxHealth, pickable.Prefab);

            if(itemWasPickedUp)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

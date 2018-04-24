using UnityEngine;

public class PlayerPickupManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickable pickable = collision.gameObject.GetComponent<Pickable>();

        if (pickable != null)
        {
            var itemWasPickedUp = GetComponent<PlayerInventory>().PickUpItem(pickable.Type, pickable.OriginalPosition, pickable.OriginalRotation, pickable.IsActive, pickable.MaxHealth, pickable.Prefab);

            if(itemWasPickedUp)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

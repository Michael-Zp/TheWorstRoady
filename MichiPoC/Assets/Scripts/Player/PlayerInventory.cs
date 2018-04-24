using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ActiveItem _activeItem;
    private List<PassiveItem> _passiveItems = new List<PassiveItem>();

    public bool PickUpItem(ItemType type, Vector3 pickupPosition, Quaternion pickupRotation, bool active, int maxHealth, GameObject prefab)
    {
        if (active && _activeItem != null)
        {
            return false;
        }

        switch (type)
        {
            case ItemType.BlueGuitar:
                _activeItem = new ActiveItem(type, pickupPosition, pickupRotation, new ItemHealth(maxHealth), prefab);
                return true;

            default:
                Debug.Log("Could not add " + type + " to inventory");
                return false;
        }
    }

    public void RemoveActiveItem()
    {
        _activeItem = null;
    }

    public ItemType GetTypeOfActiveItem()
    {
        return _activeItem.Type;
    }

    public bool HasActiveItem()
    {
        return _activeItem != null;
    }

    public void UseActiveItem()
    {
        _activeItem.Health.TakeDamage();

        if (_activeItem.Health.HasNoHpLeft())
        {
            DestroyActiveItem();
        }
    }

    public void DestroyActiveItem()
    {
        Instantiate(PickableManager.Instance.GetPrefabForType(_activeItem.Type), _activeItem.OriginalPosition, _activeItem.OriginalRotation, PickableManager.Instance.PickableParentObject.transform);
        RemoveActiveItem();
    }

    public void PunchActiveItemOutOfHand(Vector3 puncherPosition)
    {
        float direction = transform.position.x > puncherPosition.x ? 1 : -1;

        Vector3 newPosition = transform.position + new Vector3(direction * transform.localScale.x / 2.0f, transform.localScale.y / 2.0f, 0);
        GameObject obj = Instantiate(PickableManager.Instance.GetPrefabForType(_activeItem.Type), newPosition, _activeItem.OriginalRotation, PickableManager.Instance.PickableParentObject.transform);

        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 150, 75));

        obj.GetComponent<Pickable>().OriginalPosition = _activeItem.OriginalPosition;
        obj.GetComponent<Pickable>().OriginalRotation = _activeItem.OriginalRotation;

        RemoveActiveItem();
    }
}

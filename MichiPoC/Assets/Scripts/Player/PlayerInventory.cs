using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject ActiveItemRootObject;
    public HealthDisplay HealthDisplay;

    private ActiveItem __activeItem;
    private ActiveItem _activeItem {
        get {
            return __activeItem;
        }

        set {
            __activeItem = value;
            if(__activeItem != null)
            {
                _activeItemGameObject = Instantiate(__activeItem.Prefab, ActiveItemRootObject.transform);
                _activeItemGameObject.transform.localPosition = Vector3.zero;
                _activeItemGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                Type[] types = { typeof(Rigidbody2D), typeof(BoxCollider2D) };

                foreach(var type in types)
                {
                    foreach (var comp in _activeItemGameObject.GetComponents(type))
                    {
                        Destroy(comp);
                    }
                }
            }
            else
            {
                Destroy(_activeItemGameObject);
                _activeItemGameObject = null;
            }
        }
    }

    private GameObject _activeItemGameObject = null;

    private List<PassiveItem> _passiveItems = new List<PassiveItem>();
    
    public bool PickUpItem(ItemType type, Quaternion pickupRotation, bool active, int currentHealth, GameObject prefab)
    {
        if (active && _activeItem != null)
        {
            return false;
        }

        switch (type)
        {
            case ItemType.BlueGuitar:
                _activeItem = new ActiveItem(type, pickupRotation, new ItemHealth(currentHealth), prefab);
                HealthDisplay.DisplayHealth(currentHealth);
                return true;

            default:
                Debug.Log("Could not add " + type + " to inventory");
                return false;
        }
    }


    public void RemoveActiveItem()
    {
        _activeItem = null;
        HealthDisplay.DisplayHealth(0);
    }

    public bool GetTypeOfActiveItem(out ItemType type)
    {
        if(_activeItem != null)
        {
            type = _activeItem.Type;
            return true;
        }

        type = ItemType.BlueGuitar;
        return false;
    }


    public bool HasActiveItem()
    {
        return _activeItem != null;
    }

    public void UseActiveItem()
    {
        _activeItem.Health.TakeDamage();
        HealthDisplay.DisplayHealth(_activeItem.Health.CurrentHealth);

        if (_activeItem.Health.HasNoHpLeft())
        {
            DestroyActiveItem();
        }
    }

    public void DestroyActiveItem()
    {
        if (HasActiveItem())
        {
            PickableItemType pickable = PickableManager.Instance.GetPickableItemForType(_activeItem.Type);
            Instantiate(pickable.Prefab, pickable.GetRandomStartPosition().position, _activeItem.OriginalRotation, PickableManager.Instance.PickableParentObject.transform);
            RemoveActiveItem();
            HealthDisplay.DisplayHealth(0);
        }
    }

    public void PunchActiveItemOutOfHand(Vector3 puncherPosition)
    {
        if (!HasActiveItem())
        {
            return;
        }

        float direction = transform.position.x > puncherPosition.x ? 1 : -1;

        Vector3 newPosition = transform.position + new Vector3(direction * transform.localScale.x / 2.0f, transform.localScale.y / 2.0f, 0);
        GameObject obj = Instantiate(PickableManager.Instance.GetPickableItemForType(_activeItem.Type).Prefab, newPosition, _activeItem.OriginalRotation, PickableManager.Instance.PickableParentObject.transform);

        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 150, 75));
        
        obj.GetComponent<Pickable>().OriginalRotation = _activeItem.OriginalRotation;
        obj.GetComponent<Pickable>().CurrentHealth = _activeItem.Health.CurrentHealth;

        RemoveActiveItem();
    }

    public void ShowActiveItem()
    {
        _activeItemGameObject.SetActive(true);
    }

    public void HideActiveItem()
    {
        _activeItemGameObject.SetActive(false);
    }
}

using UnityEngine;

public class ActiveItem : Item
{
    public ItemHealth Health;

    public ActiveItem(ItemType type, Vector3 originalPosition, Quaternion originalRotation, ItemHealth health, GameObject prefab) : base(type, originalPosition, originalRotation, prefab)
    {
        Health = health;
    }
}

using UnityEngine;

public class ActiveItem : Item
{
    public ItemHealth Health;

    public ActiveItem(ItemType type, Quaternion originalRotation, ItemHealth health, GameObject prefab) : base(type, originalRotation, prefab)
    {
        Health = health;
    }
}

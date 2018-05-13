using UnityEngine;

public abstract class Item
{
    public ItemType Type { get; private set; }
    public Quaternion OriginalRotation { get; private set; }
    public GameObject Prefab;

    private Item()
    {

    }

    public Item(ItemType type, Quaternion originalRotation, GameObject prefab)
    {
        Type = type;
        OriginalRotation = originalRotation;
        Prefab = prefab;
    }
}

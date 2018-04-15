using UnityEngine;

public abstract class Item
{
    public ItemType Type { get; private set; }
    public Vector3 OriginalPosition { get; private set; }
    public Quaternion OriginalRotation { get; private set; }
    public GameObject Prefab;

    private Item()
    {

    }

    public Item(ItemType type, Vector3 originalPosition, Quaternion originalRotation, GameObject prefab)
    {
        Type = type;
        OriginalPosition = originalPosition;
        OriginalRotation = originalRotation;
        Prefab = prefab;
    }
}

using UnityEngine;

public class Pickable : MonoBehaviour
{
    public ItemType Type;
    public bool IsActive;
    public int MaxHealth;
    public GameObject Prefab;
    public Vector3 OriginalPosition;
    public Quaternion OriginalRotation;

    private void Awake()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
    }
}

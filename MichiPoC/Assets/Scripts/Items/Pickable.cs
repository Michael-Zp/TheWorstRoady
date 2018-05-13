using UnityEngine;

public class Pickable : MonoBehaviour
{
    public ItemType Type;
    public bool IsActive;
    public int CurrentHealth;
    public GameObject Prefab;
    public Quaternion OriginalRotation;

    private void Awake()
    {
        OriginalRotation = transform.rotation;
    }
}

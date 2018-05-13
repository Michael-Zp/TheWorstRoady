using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private static PickableManager _instance = null;
    public static PickableManager Instance {
        get {
            return _instance;
        }
    }

    public GameObject PickableParentObject;
    [SerializeField]
    private List<PickableItemType> PickableItemTypeList;

    private Dictionary<ItemType, PickableItemType> _prefabDictionary = new Dictionary<ItemType, PickableItemType>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
        

        for (int i = 0; i < PickableItemTypeList.Count; i++)
        {
            _prefabDictionary.Add(PickableItemTypeList[i].Type, PickableItemTypeList[i]);
        }
    }

    public PickableItemType GetPickableItemForType(ItemType type)
    {
        PickableItemType prefab;
        _prefabDictionary.TryGetValue(type, out prefab);

        return prefab;
    }
}

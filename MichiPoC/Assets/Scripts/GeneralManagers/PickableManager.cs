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
    private List<ItemType> ItemTypesForPrefabList;
    [SerializeField]
    private List<GameObject> PrefabsForPrefabList;

    private Dictionary<ItemType, GameObject> _prefabDictionary = new Dictionary<ItemType, GameObject>();

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



        int maxPossibleIndex = Mathf.Min(ItemTypesForPrefabList.Count, PrefabsForPrefabList.Count);

        for(int i = 0; i < maxPossibleIndex; i++)
        {
            _prefabDictionary.Add(ItemTypesForPrefabList[i], PrefabsForPrefabList[i]);
        }

        if(ItemTypesForPrefabList.Count != PrefabsForPrefabList.Count)
        {
            throw new System.ArgumentException("ItemTypesForPrefabList and PrefabForPrefabList did not contain the same amount of entries. All additional entries after index " + maxPossibleIndex + " were omitted.");
        }
    }

    public GameObject GetPrefabForType(ItemType type)
    {
        GameObject prefab;
        _prefabDictionary.TryGetValue(type, out prefab);

        return prefab;
    }
}

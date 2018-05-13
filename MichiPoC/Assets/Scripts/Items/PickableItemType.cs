using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickableItemType
{
    public ItemType Type;
    public GameObject Prefab;
    public List<Transform> StartPositions;

    private List<int> __startPositionRandomValues = null;
    private List<int> _startPositionRandomValues {
            get {
                if (__startPositionRandomValues == null)
                {
                    __startPositionRandomValues = new List<int>();
                    for (int i = 0; i < StartPositions.Count; i++)
                    {
                        __startPositionRandomValues.Add(i);
                    }
                }

                return __startPositionRandomValues;
            }           
        }

    private void Start()
    {
        for(int i = 0; i < StartPositions.Count; i++)
        {
            _startPositionRandomValues.Add(i);
        }
    }

    public Transform GetRandomStartPosition()
    {
        int randIdx = Random.Range(0, _startPositionRandomValues.Count);

        int newRand = _startPositionRandomValues[randIdx];

        for(int i = 0; i < StartPositions.Count; i++)
        {
            if(i == newRand)
            {
                continue;
            }

            _startPositionRandomValues.Add(i);
        }

        return StartPositions[newRand];
    }
}
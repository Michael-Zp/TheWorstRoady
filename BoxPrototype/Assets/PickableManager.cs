using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    public bool HasGuitar = false;

    public GameObject Guitar;
    public GameObject OriginalGuitar;
    
    public void PickUpItem(string type, GameObject pickup)
    {
        switch (type)
        {
            case "Guitar":
                OriginalGuitar = pickup;
                OriginalGuitar.SetActive(false);
                Guitar.SetActive(true);
                break;

            default:
                Debug.Log("Could not pick up type " + type);
                break;
        }
    }
}

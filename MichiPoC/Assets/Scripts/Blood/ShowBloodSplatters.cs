using UnityEngine;

public class ShowBloodSplatters : MonoBehaviour
{
    int lastA = 0;

    private void Update()
    {        
        GetComponent<SpriteRenderer>().material.SetInt("_BloodSplatterCount", EventSystem.Instance.GetBloodSplatterCount());
        GetComponent<SpriteRenderer>().material.SetVectorArray("_BloodSplatterOrigins", EventSystem.Instance.GetBloodSplatters());
        GetComponent<SpriteRenderer>().material.SetVectorArray("_BloodSplatterPatters", EventSystem.Instance.GetBloodSplatterPatterns());
    }
}

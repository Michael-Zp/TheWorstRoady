using UnityEngine;

public class ShowBloodSplatters : MonoBehaviour
{
    private void Update()
    {
        int a = EventSystem.Instance.GetBloodSplatterCount();
        var b = EventSystem.Instance.GetBloodSplatters();

        if(a != 0)
        {
            Debug.Log("");
        }

        GetComponent<SpriteRenderer>().material.SetInt("_BloodSplatterCount", EventSystem.Instance.GetBloodSplatterCount());
        GetComponent<SpriteRenderer>().material.SetVectorArray("_BloodSplatterOrigins", EventSystem.Instance.GetBloodSplatters());
    }
}

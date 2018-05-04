using UnityEngine;

public class Wobble : MonoBehaviour
{
    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = _originalScale + _originalScale  * (Mathf.Cos(Time.time * 4) * 0.1f);
    }
}

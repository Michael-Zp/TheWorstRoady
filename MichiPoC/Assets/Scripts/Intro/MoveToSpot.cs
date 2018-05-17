using UnityEngine;

public class MoveToSpot : MonoBehaviour
{

    public Transform FinalSpot;
    public float WalkTime;

    private float _startWalkTime;
    private bool _walk = false;
    private Vector3 _originalPos;

    private void Start()
    {
        _originalPos = transform.position;
    }

    public void Update()
    {
        if(_walk)
        {
            float timeRatio = (Time.time - _startWalkTime) / WalkTime;

            timeRatio = Mathf.Clamp01(timeRatio);

            transform.position = Vector3.Lerp(_originalPos, FinalSpot.position, timeRatio);
            
            GetComponent<Animator>().SetFloat("MoveDistance", Vector3.Distance(transform.position, FinalSpot.position));
        }
    }

    public void StartMoving()
    {
        _startWalkTime = Time.time;
        _walk = true;
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void StartMovingBack()
    {
        _startWalkTime = Time.time;

        Vector3 currentPos = transform.position;
        FinalSpot.position = _originalPos;
        _originalPos = currentPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}

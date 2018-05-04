using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public Vector2 DeadZone;
    public float CameraSpeed;

    public Transform RightEnd;
    public Transform LeftEnd;
    public Transform TopEnd;
    public Transform BottomEnd;

    private Vector2 _maxDeadZoneDistance;
    private float _rightMostPosition;
    private float _leftMostPosition;
    private float _topMostPosition;
    private float _bottomMostPosition;

    private void Start()
    {
        _maxDeadZoneDistance    = Camera.main.orthographicSize * 2.0f * DeadZone;
        _rightMostPosition      = RightEnd.transform.position.x  - (RightEnd.transform.localScale.x  / 2.0f) - Camera.main.orthographicSize;
        _leftMostPosition       = LeftEnd.transform.position.x   + (LeftEnd.transform.localScale.x   / 2.0f) + Camera.main.orthographicSize;
        _topMostPosition        = TopEnd.transform.position.y    - (TopEnd.transform.localScale.y    / 2.0f) - Camera.main.orthographicSize;
        _bottomMostPosition      = BottomEnd.transform.position.y + (BottomEnd.transform.localScale.y / 2.0f) + Camera.main.orthographicSize;
    }

    private void Update()
    {
        Vector2 objectToFollowPos = new Vector2(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y);
        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

        Vector2 lerped = Vector2.Lerp(cameraPos, objectToFollowPos, Time.deltaTime * CameraSpeed);

        float newPosX = cameraPos.x;
        float newPosY = cameraPos.y;
        
        if (Mathf.Abs(ObjectToFollow.transform.position.x - Camera.main.transform.position.x) > _maxDeadZoneDistance.x)
        {
            newPosX = lerped.x;
            if (newPosX > _rightMostPosition)
            {
                newPosX = _rightMostPosition;
            }

            if (newPosX < _leftMostPosition)
            {
                newPosX = _leftMostPosition;
            }
        }
        
        if (Mathf.Abs(ObjectToFollow.transform.position.y - Camera.main.transform.position.y) > _maxDeadZoneDistance.y)
        {
            newPosY = lerped.y;
            if (newPosY > _topMostPosition)
            {
                newPosY = _topMostPosition;
            }

            if (newPosY < _bottomMostPosition)
            {
                newPosY = _bottomMostPosition;
            }
        }

        Camera.main.transform.position = new Vector3(newPosX, newPosY, Camera.main.transform.position.z);
    }

}

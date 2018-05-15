using UnityEngine;

public static class TransformExtension
{
    public static Vector2 Position2D(this Transform v)
    {
        return v.position.ToVector2();
    }

    public static void SetPosition2D(this Transform v, Vector2 pos)
    {
        v.position = new Vector3(pos.x, pos.y, v.position.z);
    }
}
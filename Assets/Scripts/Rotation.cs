using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void Rotate(Vector3 dir) //자기를 중심으로 회전
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void Rotate(Vector3 dir, Vector3 center) //센터를 중심으로 회전
    {
        Rotate(dir);
        float angle = Mathf.Atan2(dir.y, dir.x);
        float distance = Vector3.Distance(center, transform.position);
        float x = distance * Mathf.Cos(angle);
        float y = distance * Mathf.Sin(angle);
        transform.localPosition = new Vector3(x, y, 0);
    }

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector2 dir = (to - from).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle - 90;
    }
}

using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float speed;

    public void MoveTo(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}

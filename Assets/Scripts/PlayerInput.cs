using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private KeyCode up = KeyCode.W;
    [SerializeField]
    private KeyCode left = KeyCode.A;
    [SerializeField]
    private KeyCode down = KeyCode.S;
    [SerializeField]
    private KeyCode right = KeyCode.D;
    [SerializeField]
    private KeyCode attack = KeyCode.Mouse0;

    public bool IsMove()
    {
        return true;
    }

    public bool IsAttack()
    {
        if (Input.GetKeyDown(attack)) return true;
        return false;
    }

    public Vector3 GetAxis()
    {
        float horizontal = 0;
        float vertical = 0;

        if (!Input.GetKey(up) || !Input.GetKey(down))
        {
            if (Input.GetKey(up)) vertical = 1;
            else if (Input.GetKey(down)) vertical = -1;
        }
        if (!Input.GetKey(left) || !Input.GetKey(right))
        {
            if (Input.GetKey(left)) horizontal = -1;
            else if (Input.GetKey(right)) horizontal = 1;
        }

        return new Vector3(horizontal, vertical, 0).normalized;
    }
}

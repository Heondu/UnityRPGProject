using UnityEngine;

public class RegenArea : MonoBehaviour
{
    [SerializeField]
    public Vector2 size;
    public int regenNumMax;
    public string monType;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
}

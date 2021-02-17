using UnityEngine;

public class Enemy : MonoBehaviour, ILivingEntity
{
    public Status status = new Status(0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    public void TakeDamage(float damage)
    {
        Debug.Log($"{name} : {damage}");
    }

    public Status GetStatus()
    {
        return status;
    }
}

using UnityEngine;

public class Attack : MonoBehaviour
{
    public void Execute(int damage)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 3);
        if (collider.gameObject != gameObject && collider.GetComponent<ILivingEntity>() != null)
            collider.GetComponent<ILivingEntity>().TakeDamage(damage);

        Debug.Log($"[공격] 데미지 {damage}!");
    }
}

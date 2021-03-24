using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rotation rotation;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
    }

    public void Execute(int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, transform.rotation.z);
        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject != transform.root.gameObject)
            {
                ILivingEntity entity = collider.GetComponent<ILivingEntity>();

                if (entity != null)
                {
                    entity.TakeDamage();
                }
            }
        }
    }
}

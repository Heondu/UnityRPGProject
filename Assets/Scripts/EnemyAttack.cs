using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float cooltime;
    private float currentTime = 0;
    private bool isCool = false;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    public void Execute(int damage)
    {
        if (isCool) return;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.one * attackRange, Rotation.GetAngle(transform.position, player.position));
        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject == transform.root.gameObject) continue;
            if (collider.CompareTag("Player") == false) continue;
            collider.GetComponent<ILivingEntity>().TakeDamage(damage);
            isCool = true;
            StartCoroutine("Cooltime");
        }
    }

    private IEnumerator Cooltime()
    {
        currentTime = 0;
        while (currentTime < cooltime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        isCool = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (player.position - transform.position).normalized * attackRange);
    }
}

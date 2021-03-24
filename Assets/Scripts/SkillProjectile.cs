using System.Collections;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    [SerializeField]
    private float radius;
    private GameObject executor;
    private Skill skill;
    private GameObject target;
    private Movement movement;
    private float speed = 6;
    private float speedMin = 3;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        movement.Execute(transform.up);
        movement.SetMoveSpeed(Mathf.Max(speedMin, speed));
        SetDir();
    }

    public void Execute(float angle, GameObject executor, Skill skill)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.executor = executor;
        this.skill = skill;
        StartCoroutine("FindTarget");
        StartCoroutine("SetSpeed");
    }

    private void SetDir()
    {
        if (target == null) return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    private IEnumerator SetSpeed()
    {
        while (speed > speedMin)
        {
            speed -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            target = null;
            float distance = Mathf.Infinity;
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == executor) continue;
                if (collider.gameObject == gameObject) continue;

                ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                if (entity == null) continue;
                if (distance > Vector3.SqrMagnitude(collider.transform.position - transform.position))
                {
                    target = collider.gameObject;
                    distance = Vector3.SqrMagnitude(target.transform.position - transform.position);
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            ILivingEntity entity = collision.GetComponent<ILivingEntity>();
            StatusCalculator.SkillStatusCalc(executor.GetComponent<Status>().status, collision.GetComponent<Status>().status, skill);
            entity.TakeDamage();
            Destroy(gameObject);
        }
    }
}

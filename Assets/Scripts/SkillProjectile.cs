using System.Collections;
using UnityEngine;

public class SkillProjectile : SkillScript
{
    [SerializeField]
    private float radius;
    private GameObject target;
    private Movement movement;
    private int penetrationCount = 0;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    protected override void Update()
    {
        base.Update();
        movement.Execute(transform.up, skill.speed);
        SetDir();
    }

    public override void Execute(GameObject executor, Skill skill)
    {
        base.Execute(executor, skill);
        float angle = Rotation.GetAngle(executor.transform.position);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        StartCoroutine("FindTarget");
    }

    private void SetDir()
    {
        if (target == null) return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * skill.guide);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            penetrationCount++;
            for (int i = 0; i < nextSkills.Length; i++)
                if (nextSkills[i] != "") SkillLoader.SkillLoad(executor, DataManager.skillDB[nextSkills[i]], transform.position);
            if (skill.penetration <= penetrationCount) Destroy(gameObject);
        }
    }
}

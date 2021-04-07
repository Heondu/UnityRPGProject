using System.Collections;
using UnityEngine;

public class SkillExplode : SkillScript
{
    [SerializeField]
    private float radius;
    private int penetrationCount = 0;

    protected override void Update()
    {
        if (skill != null)
        {
            if (timer.IsTimeOut(skill.lifetime)) Destroy(gameObject);
        }
        if (skill.penetration <= penetrationCount) Destroy(gameObject);
    }

    public override void Execute(GameObject executor, Skill skill)
    {
        base.Execute(executor, skill);
        StartCoroutine("CoExecute");
    }

    private IEnumerator CoExecute()
    {
        while (true)
        {
            if (skill.isPositive == 1)
            {
                ILivingEntity entity = executor.GetComponent<ILivingEntity>();
                CalcSkillStatus(entity);
                penetrationCount++;
            }
            else if (skill.isPositive == 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject == executor) continue;
                    if (collider.gameObject == gameObject) continue;

                    ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                    if (entity == null) continue;
                    CalcSkillStatus(entity);
                    penetrationCount++;
                }
            }

            yield return new WaitForSeconds(skill.delay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

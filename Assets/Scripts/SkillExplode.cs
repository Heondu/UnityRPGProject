﻿using System.Collections;
using UnityEngine;

public class SkillExplode : SkillScript
{
    [SerializeField]
    private float radius;
    private int penetrationCount = 0;

    protected override void Update()
    {
        base.Update();
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D collider in colliders)
            {
                if (skill.isPositive == 0)
                {
                    if (collider.gameObject == executor) continue;
                }
                if (collider.gameObject == gameObject) continue;

                ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                if (entity == null) continue;
                //int damage = StatusCalculator.SkillStatusCalc(executor.GetComponent<Status>().status, collider.GetComponent<Status>().status, skill);
                entity.TakeDamage(10);

                penetrationCount++;
                if (skill.penetration <= penetrationCount) Destroy(gameObject);
            }

            yield return new WaitForSeconds(skill.delay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

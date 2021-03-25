﻿using System.Collections;
using UnityEngine;

public class SkillExplode : SkillScript
{
    [SerializeField]
    private float radius = 5;
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
                if (collider.gameObject == executor) continue;
                if (collider.gameObject == gameObject) continue;

                ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                if (entity == null) continue;
                StatusCalculator.SkillStatusCalc(executor.GetComponent<Status>().status, collider.GetComponent<Status>().status, skill);
                entity.TakeDamage();

                penetrationCount++;
                if ((int)skill.status["penetration"] <= penetrationCount) Destroy(gameObject);
            }

            yield return new WaitForSeconds(float.Parse(skill.status["delay"].ToString()));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
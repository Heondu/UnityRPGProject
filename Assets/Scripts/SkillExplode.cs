using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExplode : MonoBehaviour
{
    [SerializeField]
    private float radius;

    public void Execute(Vector3 pos, GameObject executor, Skill skill)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == executor) continue;
            if (collider.gameObject == gameObject) continue;

            ILivingEntity entity = collider.GetComponent<ILivingEntity>();
            if (entity == null) continue;
            StatusCalculator.SkillStatusCalc(executor.GetComponent<Status>().status, collider.GetComponent<Status>().status, skill);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

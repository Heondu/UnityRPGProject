using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject attacker;
    public Skill skill;

    public void Init()
    {
        Destroy(gameObject, skill.skillStatus.coolTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != attacker)
        {
            SkillStatus.DamageCalc(attacker.GetComponent<ILivingEntity>().GetStatus(), collision.GetComponent<ILivingEntity>(), skill.skillStatus);
        }
    }
}

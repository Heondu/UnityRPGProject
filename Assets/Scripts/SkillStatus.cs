using UnityEngine;

public enum Type { fire = 0, water, dark, bright };

[System.Serializable]
public class SkillStatus
{
    public Type type;
    public float typeStrenght;
    public float magnitude;
    public bool isAbleCriticalHit;
    public int randDirection;
    public bool isGuidedMissile;
    public int cast;
    public float coolTime;
    public float boundsMultiplier;
    public float rangeMultiplier;
    public float speedMultiplier;

    public SkillStatus(Type type, float typeStrenght, float magnitude, bool isAbleCriticalHit, int randDirection, bool isGuidedMissile, int cast, float coolTime, float boundsMultiplier, float rangeMultiplier, int speedMultiplier)
    {
        this.type = type;
        this.typeStrenght = typeStrenght;
        this.magnitude = magnitude;
        this.isAbleCriticalHit = isAbleCriticalHit;
        this.randDirection = randDirection;
        this.isGuidedMissile = isGuidedMissile;
        this.cast = cast;
        this.coolTime = coolTime;
        this.boundsMultiplier = boundsMultiplier;
        this.rangeMultiplier = rangeMultiplier;
        this.speedMultiplier = speedMultiplier;
    }

    public static void DamageCalc(Status attaker, ILivingEntity victim, SkillStatus skill)
    {
        float damage = 0;
        damage += attaker.ATK + skill.magnitude;
        victim.TakeDamage(damage);
    }
}

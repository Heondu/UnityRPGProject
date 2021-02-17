using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillStatus skillStatus = new SkillStatus(Type.fire, 0, 0, false, 0, false, 0, 0, 0, 0, 0);
    private string name;

    public Skill(string name, Type type, float typeStrenght, float magnitude, bool isAbleCriticalHit, int randDirection, bool isGuidedMissile, int cast, float coolTime, float boundsMultiplier, float rangeMultiplier, int speedMultiplier)
    {
        this.name = name;

        skillStatus.type = type;
        skillStatus.typeStrenght = typeStrenght;
        skillStatus.magnitude = magnitude;
        skillStatus.isAbleCriticalHit = isAbleCriticalHit;
        skillStatus.randDirection = randDirection;
        skillStatus.isGuidedMissile = isGuidedMissile;
        skillStatus.cast = cast;
        skillStatus.coolTime = coolTime;
        skillStatus.boundsMultiplier = boundsMultiplier;
        skillStatus.rangeMultiplier = rangeMultiplier;
        skillStatus.speedMultiplier = speedMultiplier;
    }
}

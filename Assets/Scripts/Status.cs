using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    public int strength;
    public int agility;
    public int intelligence;
    public int endurance;
    public int damage;
    public int fixDam;
    public int critChance;
    public int avoidance;
    public int accuracy;
    public int reduceMana;
    public int reduceCooltime;
    public int defence;
    public int allResist;
    private const float statusMultiplier = 0.05f;

    public void Init()
    {
        damage = strength;
        fixDam = Mathf.RoundToInt(strength * statusMultiplier);
        critChance = Mathf.RoundToInt(agility * statusMultiplier);
        avoidance = Mathf.RoundToInt(agility * statusMultiplier);
        accuracy = Mathf.RoundToInt(agility * statusMultiplier);
        reduceMana = Mathf.RoundToInt(intelligence * statusMultiplier);
        reduceCooltime = Mathf.RoundToInt(intelligence * statusMultiplier);
        defence = endurance;
        allResist = Mathf.RoundToInt(endurance * statusMultiplier);
    }
}

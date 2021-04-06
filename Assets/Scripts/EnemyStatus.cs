﻿[System.Serializable]
public class EnemyStatus : BaseStatus
{
    public int HP;
    public int maxHP;

    public Status strength;
    public Status agility;
    public Status intelligence;
    public Status endurance;
    public Status damage;
    public Status defence;
    public Status allResist;
    public Status fireResist;
    public Status coldResist;
    public Status darkResist;
    public Status lightResist;
    public Status fireDamage;
    public Status coldDamage;
    public Status darkDamage;
    public Status lightDamage;
    public Status fixDamage;
    public Status critChance;
    public Status critResist;
    public Status critDamage;
    public Status avoidance;
    public Status accuracy;
    public Status reduceMana;
    public Status reduceCool;

    private const float multValue = 0.05f;

    public void CalculateDerivedStatus()
    {
        damage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat));
        fixDamage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat));
        critChance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat));
        avoidance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat));
        accuracy.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat));
        reduceMana.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat));
        reduceCool.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat));
        defence.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat));
        allResist.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat));

        fixDamage.AddModifier(new StatusModifier(multValue, StatusModType.PercentMult));
        critChance.AddModifier(new StatusModifier(multValue, StatusModType.Flat));
        avoidance.AddModifier(new StatusModifier(multValue, StatusModType.Flat));
        accuracy.AddModifier(new StatusModifier(multValue, StatusModType.Flat));
        reduceMana.AddModifier(new StatusModifier(multValue, StatusModType.Flat));
        reduceCool.AddModifier(new StatusModifier(multValue, StatusModType.Flat));
        allResist.AddModifier(new StatusModifier(multValue, StatusModType.Flat));
    }
}

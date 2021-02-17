using UnityEngine;

public class Item
{
    public Status status = new Status(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    private string name;

    public Item(string name, float ATK, float criticalHitChance, float globalCriticalMultiplier, float DEF, float HP, float SPI,
              float fireTypeStrength, float waterTypeStrength, float darkTypeStrength, float brightTypeStrength,
              float fireTypeREG, float waterTypeREG, float darkTypeREG, float brightTypeREG, float durabilityNegation,
              float damageMultiplier, float ATKMultiplier)
    {
        this.name = name;

        status.ATK = ATK;
        status.criticalHitChance = criticalHitChance;
        status.globalCriticalMultiplier = globalCriticalMultiplier;
        status.DEF = DEF;
        status.HP = HP;
        status.SPI = SPI;
        status.fireTypeStrength = fireTypeStrength;
        status.waterTypeStrength = waterTypeStrength;
        status.darkTypeStrength = darkTypeStrength;
        status.brightTypeStrength = brightTypeStrength;
        status.fireTypeREG = fireTypeREG;
        status.waterTypeREG = waterTypeREG;
        status.darkTypeREG = darkTypeREG;
        status.brightTypeREG = brightTypeREG;
        status.durabilityNegation = durabilityNegation;
        status.damageMultiplier = damageMultiplier;
        status.ATKMultiplier = ATKMultiplier;
    }
}

[System.Serializable]
public class Status
{
    public float ATK;                      //공격력
    public float criticalHitChance;        //치명타 확률
    public float globalCriticalMultiplier; //치명타 배율
    public float DEF;                      //방어력
    public float HP;                       //체력
    public float SPI;                      //정신력
    public float fireTypeStrength;         //화 속성 강화
    public float waterTypeStrength;        //수 속성 강화
    public float darkTypeStrength;         //암 속성 강화
    public float brightTypeStrength;       //명 속성 강화
    public float fireTypeREG;              //화 속성 저항
    public float waterTypeREG;             //수 속성 저항
    public float darkTypeREG;              //암 속성 저항
    public float brightTypeREG;            //명 속성 저항
    public float durabilityNegation;       //방어 무시
    public float damageMultiplier;         //받는 피해 배율
    public float ATKMultiplier;            //입히는 피해 배율

    public Status(float ATK, float criticalHitChance, float globalCriticalMultiplier, float DEF, float HP, float SPI,
                  float fireTypeStrength, float waterTypeStrength, float darkTypeStrength, float brightTypeStrength,
                  float fireTypeREG, float waterTypeREG, float darkTypeREG, float brightTypeREG, float durabilityNegation,
                  float damageMultiplier, float ATKMultiplier)
    {
        this.ATK                        = ATK;
        this.criticalHitChance          = criticalHitChance;
        this.globalCriticalMultiplier   = globalCriticalMultiplier;
        this.DEF                        = DEF;
        this.HP                         = HP;
        this.SPI                        = SPI;
        this.fireTypeStrength           = fireTypeStrength;
        this.waterTypeStrength          = waterTypeStrength;
        this.darkTypeStrength           = darkTypeStrength;
        this.brightTypeStrength         = brightTypeStrength;
        this.fireTypeREG                = fireTypeREG;
        this.waterTypeREG               = waterTypeREG;
        this.darkTypeREG                = darkTypeREG;
        this.brightTypeREG              = brightTypeREG;
        this.durabilityNegation         = durabilityNegation;
        this.damageMultiplier           = damageMultiplier;
        this.ATKMultiplier              = ATKMultiplier;
    }

    public static void StatusCalc(Status targetStatus, Status status, bool isAdd)
    {
        if (isAdd)
        {
            targetStatus.ATK                        += status.ATK;
            targetStatus.criticalHitChance          += status.criticalHitChance;       
            targetStatus.globalCriticalMultiplier   += status.globalCriticalMultiplier;
            targetStatus.DEF                        += status.DEF;                     
            targetStatus.HP                         += status.HP;                      
            targetStatus.SPI                        += status.SPI;                     
            targetStatus.fireTypeStrength           += status.fireTypeStrength;        
            targetStatus.waterTypeStrength          += status.waterTypeStrength;       
            targetStatus.darkTypeStrength           += status.darkTypeStrength;        
            targetStatus.brightTypeStrength         += status.brightTypeStrength;      
            targetStatus.fireTypeREG                += status.fireTypeREG;             
            targetStatus.waterTypeREG               += status.waterTypeREG;            
            targetStatus.darkTypeREG                += status.darkTypeREG;             
            targetStatus.brightTypeREG              += status.brightTypeREG;           
            targetStatus.durabilityNegation         += status.durabilityNegation;      
            targetStatus.damageMultiplier           += status.damageMultiplier;        
            targetStatus.ATKMultiplier              += status.ATKMultiplier;           
        }
        else
        {
            targetStatus.ATK                        -= status.ATK;
            targetStatus.criticalHitChance          -= status.criticalHitChance;
            targetStatus.globalCriticalMultiplier   -= status.globalCriticalMultiplier;
            targetStatus.DEF                        -= status.DEF;
            targetStatus.HP                         -= status.HP;
            targetStatus.SPI                        -= status.SPI;
            targetStatus.fireTypeStrength           -= status.fireTypeStrength;
            targetStatus.waterTypeStrength          -= status.waterTypeStrength;
            targetStatus.darkTypeStrength           -= status.darkTypeStrength;
            targetStatus.brightTypeStrength         -= status.brightTypeStrength;
            targetStatus.fireTypeREG                -= status.fireTypeREG;
            targetStatus.waterTypeREG               -= status.waterTypeREG;
            targetStatus.darkTypeREG                -= status.darkTypeREG;
            targetStatus.brightTypeREG              -= status.brightTypeREG;
            targetStatus.durabilityNegation         -= status.durabilityNegation;
            targetStatus.damageMultiplier           -= status.damageMultiplier;
            targetStatus.ATKMultiplier              -= status.ATKMultiplier;
        }
    }
}

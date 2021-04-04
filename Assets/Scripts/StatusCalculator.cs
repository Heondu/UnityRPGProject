using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCalculator : MonoBehaviour
{
    private static string[] statusString = { "strength", "agility", "intelligence", "endurance", "damage", "fixDamage",
                                    "fireDamage", "coldDamage", "darkDamage", "lightDamage", "critChance",
                                    "critResist", "critDamage", "avoidance", "accuracy", "reduceMana", "reduceCool",
                                    "defence", "fireResist", "coldResist", "darkResist", "lightResist", "allResist" };
    private static string[] statusPerString = { "strengthPer", "agilityPer", "intelligencePer", "endurancePer", "damagePer", "fixDamagePer",
                                    "fireDamagePer", "coldDamagePer", "darkDamagePer", "lightDamagePer", "critChancePer",
                                    "critResistPer", "critDamagePer", "avoidancePer", "accuracyPer", "reduceManaPer", "reduceCoolPer",
                                    "defencePer", "fireResistPer", "coldResistPer", "darkResistPer", "lightResistPer", "allResistPer" };
    private const float statusMultiplier = 0.05f;

    public static void Init(Dictionary<string, float> status, Dictionary<string, float> fourStatus)
    {
        for (int i = 0; i < 4; i++)
        {
            status[statusString[i]] = fourStatus[statusString[i]];
        }
        for (int i = 4; i < statusString.Length; i++)
        {
            status[statusString[i]] = 0;
        }
        for (int i = 0; i < statusPerString.Length; i++)
        {
            status[statusPerString[i]] = 0;
        }
    }

    private static void DeriveStatusCalc(Dictionary<string, float> status)
    {
        status["damage"] += status["strength"];
        status["fixDamage"] += status["strength"] * statusMultiplier;
        status["critChance"] += status["agility"] * statusMultiplier;
        status["avoidance"] += status["agility"] * statusMultiplier;
        status["accuracy"] += status["agility"] * statusMultiplier;
        status["reduceMana"] += status["intelligence"] * statusMultiplier;
        status["reduceCool"] += status["intelligence"] * statusMultiplier;
        status["defence"] += status["endurance"];
        status["allResist"] += status["endurance"] * statusMultiplier;
    }

    private static void PercentStatusCalc(Dictionary<string, float> status)
    {
        for (int i = 0; i < 4; i++)
        {
            status[statusString[i]] = status[statusString[i]] * (1 + status[statusPerString[i]] / 100);
        }
        DeriveStatusCalc(status);
        for (int i = 4; i < statusString.Length; i++)
        {
            status[statusString[i]] = status[statusString[i]] * (1 + status[statusPerString[i]] / 100);
        }
    }

    private static void ItemStatusCalc(Dictionary<string, float> status, Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                status[items[i].status] += items[i].stat;

                for (int j = 0; j < items[i].statusAdd.Length; j++)
                {
                    status[items[i].statusAdd[j]] += items[i].statAdd[j];
                }
            }
        }
    }

    public static int SkillStatusCalc(Dictionary<string, float> executorStatus, Dictionary<string, float> targetStatus, Skill skill)
    {
        for (int i = 0; i <= 1; i++)
        {
            int value;
            if (skill.relatedStatus[i] == "") continue;
            if (skill.status[i] == "none") continue;
            else if (skill.relatedStatus[i] == "none") value = skill.amount[i];
            else value = Mathf.RoundToInt(executorStatus[skill.relatedStatus[i]] * (skill.amount[i] / 100));

            if (skill.isPositive == 1) executorStatus[skill.status[i]] += value;
            else if (skill.isPositive == 0)
            {
                if (skill.status[i] == "HP") return value;
                else targetStatus[skill.status[i]] -= value;
            }
        }
        return 0;
    }

    public static void SkillsStatusCalc(Dictionary<string, float> executorStatus, Skill[] skills)
    {
        if (skills == null) return;

        foreach (Skill skill in skills)
        {
            for (int i = 1; i <= 2; i++)
            {
                float value;
                if (skill.relatedStatus[i] == "") continue;
                if (skill.status[i] == "none") continue;
                else if (skill.relatedStatus[i] == "none") value = skill.amount[i];
                else value = Mathf.RoundToInt(executorStatus[skill.relatedStatus[i]] * (skill.amount[i] / 100));

                if (skill.isPositive == 1) executorStatus[skill.status[i]] += value;
            }
        }
    }

    private static void Round(Dictionary<string, float> status)
    {
        for (int i = 0; i < statusString.Length; i++)
        {
            status[statusString[i]] = Mathf.Round(status[statusString[i]]);
        }
    }

    public static void StatusCalc(Dictionary<string, float> status, Dictionary<string, float> baseStatus, Item[] items = null, Skill[] skills = null)
    {
        Init(status, baseStatus);
        if (items != null) ItemStatusCalc(status, items);
        PercentStatusCalc(status);
        Round(status);
        SkillsStatusCalc(status, skills);
    }
}

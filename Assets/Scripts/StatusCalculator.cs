using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCalculator : MonoBehaviour
{
    private static string[] statusString = { "strength", "agility", "intelligence", "endurance", "damage", "fixDam",
                                    "fireDamage", "coldDamage", "darkDamage", "lightDamage", "critChance",
                                    "critAvoid", "critDamage", "avoidance", "accuracy", "reduceMana", "reduceCooltime",
                                    "defence", "fireResist", "coldResist", "darkResist", "lightResist", "allResist" };
    private static string[] statusPerString = { "strength%", "agility%", "intelligence%", "endurance%", "damage%", "fixDam%",
                                    "fireDamage%", "coldDamage%", "darkDamage%", "lightDamage%", "critChance%",
                                    "critAvoid%", "critDamage%", "avoidance%", "accuracy%", "reduceMana%", "reduceCooltime%",
                                    "defence%", "fireResist%", "coldResist%", "darkResist%", "lightResist%", "allResist%" };
    private const float statusMultiplier = 0.05f;

    public static void Init(StringFloat status, Dictionary<string, object> fourStatus)
    {
        for (int i = 0; i < 4; i++)
        {
            status[statusString[i]] = (int)fourStatus[statusString[i]];
        }
        for (int i = 4; i < statusString.Length; i++)
        {
            status[statusString[i]] = 0;
        }
        for (int i = 0; i < statusPerString.Length; i++)
        {
            status[statusPerString[i]] = 0;
        }
        status["HP"] = 10;
    }

    private static void DeriveStatusCalc(StringFloat status)
    {
        status["damage"] += status["strength"];
        status["fixDam"] += status["strength"] * statusMultiplier;
        status["critChance"] += status["agility"] * statusMultiplier;
        status["avoidance"] += status["agility"] * statusMultiplier;
        status["accuracy"] += status["agility"] * statusMultiplier;
        status["reduceMana"] += status["intelligence"] * statusMultiplier;
        status["reduceCooltime"] += status["intelligence"] * statusMultiplier;
        status["defence"] += status["endurance"];
        status["allResist"] += status["endurance"] * statusMultiplier;
    }

    private static void PercentStatusCalc(StringFloat status)
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

    private static void ItemStatusCalc(StringFloat status, Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name != "")
            {
                status[items[i].status] += items[i].stat;

                for (int j = 0; j < items[i].statusAdd.Length; j++)
                {
                    status[items[i].statusAdd[j]] += items[i].statAdd[j];
                }
            }
        }
    }

    public static void SkillStatusCalc(StringFloat executorStatus, StringFloat targetStatus, Skill skill)
    {
        for (int i = 0; i <= 1; i++)
        {
            float value;
            if (skill.relatedStatus[i] == "") continue;
            if (skill.status[i] == "none") continue;
            else if (skill.relatedStatus[i] == "none") value = skill.amount[i];
            else value = Mathf.RoundToInt(executorStatus[skill.relatedStatus[i]] * (skill.amount[i] / 100));

            if (skill.isPositive == 1)
            {
                Debug.Log($"{skill.skill} : {value}");
                executorStatus[skill.status[i]] += value;
                Debug.Log($"Executor : {executorStatus[skill.status[i]]}");
            }
            else if (skill.isPositive == 0) targetStatus[skill.status[i]] -= value;
        }
    }

    public static void SkillsStatusCalc(StringFloat executorStatus, List<Skill> skills)
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

    private static void Round(StringFloat status)
    {
        for (int i = 0; i < statusString.Length; i++)
        {
            status[statusString[i]] = Mathf.Round(status[statusString[i]]);
        }
    }

    public static void StatusCalc(StringFloat status, Dictionary<string, object> fourStatus, Item[] items = null, List<Skill> skills = null)
    {
        Init(status, fourStatus);
        if (items != null) ItemStatusCalc(status, items);
        PercentStatusCalc(status);
        Round(status);
        SkillsStatusCalc(status, skills);
    }
}

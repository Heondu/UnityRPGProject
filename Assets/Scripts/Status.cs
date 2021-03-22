using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringFloat : SerializableDictionary<string, float> { }

public class Status : MonoBehaviour
{
    public StringFloat status;
    private string[] statusString = { "strength", "agility", "intelligence", "endurance", "damage", "fixDam",
                                    "fireDamage", "coldDamage", "darkDamage", "lightDamage", "critChance",
                                    "critAvoid", "critDamage", "avoidance", "accuracy", "reduceMana", "reduceCooltime",
                                    "defence", "fireResist", "coldResist", "darkResist", "lightResist", "allResist" };
    private string[] statusPerString = { "strength%", "agility%", "intelligence%", "endurance%", "damage%", "fixDam%",
                                    "fireDamage%", "coldDamage%", "darkDamage%", "lightDamage%", "critChance%",
                                    "critAvoid%", "critDamage%", "avoidance%", "accuracy%", "reduceMana%", "reduceCooltime%",
                                    "defence%", "fireResist%", "coldResist%", "darkResist%", "lightResist%", "allResist%" };
    private const float statusMultiplier = 0.05f;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        status["strength"] = 10;
        for (int i = 1; i < statusString.Length; i++)
        {
            status[statusString[i]] = 0;
        }
        for (int i = 0; i < statusPerString.Length; i++)
        {
            status[statusPerString[i]] = 0;
        }
    }

    private void DeriveStatusCalc()
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

    private void PercentStatusCalc()
    {
        for (int i = 0; i < 4; i++)
        {
            status[statusString[i]] = status[statusString[i]] * (1 + status[statusPerString[i]] / 100);
        }
        DeriveStatusCalc();
        for (int i = 4; i < statusString.Length; i++)
        {
            status[statusString[i]] = status[statusString[i]] * (1 + status[statusPerString[i]] / 100);
        }
    }

    private void ItemStatusCalc(Item[] items)
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

    private void Round()
    {
        for (int i = 0; i < statusString.Length; i++)
        {
            status[statusString[i]] = Mathf.Round(status[statusString[i]]);
        }
    }

    public void StatusCalc()
    {
        Init();
        PercentStatusCalc();
        Round();
    }

    public void StatusCalc(Item[] items)
    {
        Init();
        ItemStatusCalc(items);
        PercentStatusCalc();
        Round();
    }
}

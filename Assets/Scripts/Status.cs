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
    private Dictionary<string, int> status = new Dictionary<string, int>();

    private void Init()
    {
        status["damage"] = strength;
        status["fixDam"] = Mathf.RoundToInt(strength * statusMultiplier);
        status["critChance"] = Mathf.RoundToInt(agility * statusMultiplier);
        status["avoidance"] = Mathf.RoundToInt(agility * statusMultiplier);
        status["accuracy"] = Mathf.RoundToInt(agility * statusMultiplier);
        status["reduceMana"] = Mathf.RoundToInt(intelligence * statusMultiplier);
        status["reduceCooltime"] = Mathf.RoundToInt(intelligence * statusMultiplier);
        status["defence"] = endurance;
        status["allResist"] = Mathf.RoundToInt(endurance * statusMultiplier);
    }

    public void Apply()
    {
        damage = status["damage"];
        fixDam = status["fixDam"];
        critChance = status["critChance"];
        avoidance = status["avoidance"];
        accuracy = status["accuracy"];
        reduceMana = status["reduceMana"];
        reduceCooltime = status["reduceCooltime"];
        defence = status["defence"];
        allResist = status["allResist"];
    }

    public void StatusCalc()
    {
        Init();
        Apply();
    }

    public void StatusCalc(Item[] items)
    {
        Init();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name != "")
            {
                if (items[i].status.Contains("%")) status[items[i].status] *= items[i].stat;
                else status[items[i].status] += items[i].stat;
            }
        }
        Apply();
    }
}

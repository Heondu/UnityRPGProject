using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public Dictionary<string, float> status = new Dictionary<string, float>();

    private void Awake()
    {
        status["strength"] = 0;
        status["agility"] = 0;
        status["intelligence"] = 0;
        status["endurance"] = 0;
        status["damage"] = 0;
        status["defence"] = 0;
        status["allResist"] = 0;
        status["fireResist"] = 0;
        status["coldResist"] = 0;
        status["darkResist"] = 0;
        status["lightResist"] = 0;
        status["fireDamage"] = 0;
        status["coldDamage"] = 0;
        status["darkDamage"] = 0;
        status["lightDamage"] = 0;
        status["fixDamage"] = 0;
        status["critChance"] = 0;
        status["avoidance"] = 0;
        status["accuracy"] = 0;
        status["reduceMana"] = 0;
        status["reduceCool"] = 0;
        status["critResist"] = 0;
        status["critDamage"] = 0;
        status["experience"] = 0;
        status["itemRange"] = 0;
        status["maxHp"] = 0;
        status["HP"] = 0;
        status["maxMana"] = 0;
        status["mana"] = 0;
        status["exp"] = 0;
        status["level"] = 0;
        status["strength%"] = 0;
        status["agility%"] = 0;
        status["intelligence%"] = 0;
        status["endurance%"] = 0;
        status["damage%"] = 0;
        status["defence%"] = 0;
        status["allResist%"] = 0;
        status["fireResist%"] = 0;
        status["coldResist%"] = 0;
        status["darkResist%"] = 0;
        status["lightResist%"] = 0;
        status["fireDamage%"] = 0;
        status["coldDamage%"] = 0;
        status["darkDamage%"] = 0;
        status["lightDamage%"] = 0;
        status["fixDamage%"] = 0;
        status["critChance%"] = 0;
        status["avoidance%"] = 0;
        status["accuracy%"] = 0;
        status["reduceMana%"] = 0;
        status["reduceCool%"] = 0;
        status["critResist%"] = 0;
        status["critDamage%"] = 0;
        status["experience%"] = 0;
        status["itemRange%"] = 0;
        status["maxHp%"] = 0;
        status["HP%"] = 0;
        status["maxMana%"] = 0;
        status["mana%"] = 0;
    }
}
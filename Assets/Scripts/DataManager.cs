using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static List<Dictionary<string, object>> weapon = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> armor = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> accessories = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> additional = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> experience = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> monlvl = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> monster = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> rarity = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> droptable = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> localization_KOR = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> ore = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> runes = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> skills = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> skillexp = new List<Dictionary<string, object>>();

    public static List<Dictionary<string, object>> item = new List<Dictionary<string, object>>();
    public static Dictionary<string, Item> itemDB = new Dictionary<string, Item>();
    public static Dictionary<string, Skill> skillDB = new Dictionary<string, Skill>();

    private void Awake()
    {
        weapon = CSVReader.Read("weapon");
        armor = CSVReader.Read("armor");
        accessories = CSVReader.Read("accessories");
        additional = CSVReader.Read("additional");
        experience = CSVReader.Read("experience");
        monlvl = CSVReader.Read("monlvl");
        monster = CSVReader.Read("monster");
        rarity = CSVReader.Read("rarity");
        droptable = CSVReader.Read("droptable");
        localization_KOR = CSVReader.Read("localization_KOR");
        ore = CSVReader.Read("ore");
        runes = CSVReader.Read("runes");
        skills = CSVReader.Read("skills");
        skillexp = CSVReader.Read("skillexp");

        ListToDict(weapon);
        ListToDict(armor);
        ListToDict(accessories);
        ListToDict(skills);
    }

    private void ListToDict(List<Dictionary<string, object>> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list == weapon || list == armor || list == accessories)
            {
                Dictionary<string, object> temp = list[i];
                item.Add(temp);
                string name = list[i]["name"].ToString();
                itemDB[name] = new Item
                {
                    name = name,
                    spawnable = (int)list[i]["spawnable"],
                    type = list[i]["type"].ToString(),
                    status = list[i]["status"].ToString(),
                    rarity = (int)list[i]["rarity"],
                    statMin = (int)list[i]["statMin"],
                    statMax = (int)list[i]["statMax"],
                    cost = (int)list[i]["cost"]
                };
            }
            else if (list == skills)
            {
                string name = list[i]["name"].ToString();
                skillDB[name] = new Skill();
                skillDB[name].status = list[i];
            }
        }
    }

    public static Dictionary<string, object> Find(List<Dictionary<string, object>> list, string key, object value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return list[i];
        }
        return null;
    }

    public static bool Exists(List<Dictionary<string, object>> list, string key, object value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return true;
        }
        return false;
    }
}

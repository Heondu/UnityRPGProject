using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator
{
    [SerializeField]
    private static Item item;
    private static Dictionary<string, object> rarity;
    private static Dictionary<string, object> rarityAdd;
    private static List<string> itemList = new List<string>();
    private static List<string> additionalList = new List<string>();
    private static string rarityType = "Normal";

    public static Item DropItem(int rarityMin, int rarityMax, string type)
    {
        Filtering(rarityMin, rarityMax);
        RandomRarity(type);
        ItemInit();
        Additional();
        return item;
    }

    private static void Filtering(int rarityMin, int rarityMax)
    {
        for (int i = 0; i < DataManager.item.Count; i++)
        {
            if (rarityMin <= (int)DataManager.item[i]["rarity"] &&
                rarityMax >= (int)DataManager.item[i]["rarity"])
                itemList.Add(DataManager.item[i]["name"].ToString());
        }
    }

    private static void RandomRarity(string type)
    {
        rarity = DataManager.Find(DataManager.rarity, "Function", type);
        int[] sort = { (int)rarity["Legendary"], (int)rarity["Unique"], (int)rarity["Rare"], (int)rarity["Magic"], (int)rarity["HiQuality"], (int)rarity["Normal"] };
        string[] types = { "Legendary", "Unique", "Rare", "Magic", "HiQuality", "Normal" };
        for (int i = 0; i < sort.Length - 1; i++) {
            for (int j = i + 1; j < sort.Length; j++) {
                if (sort[i] > sort[j]) {
                    int tempInt = sort[i];
                    sort[i] = sort[j];
                    sort[j] = tempInt;
                    string tempString = types[i];
                    types[i] = types[j];
                    types[j] = tempString;
                }
            }
        }
        int rand = Random.Range(0, sort[sort.Length - 1]);
        if (rand <= sort[0]) rarityType = types[0];
        else if (rand <= sort[1]) rarityType = types[1];
        else if (rand <= sort[2]) rarityType = types[2];
        else if (rand <= sort[3]) rarityType = types[3];
        else if (rand <= sort[4]) rarityType = types[4];
        else if (rand <= sort[5]) rarityType = types[5];
    }

    private static void ItemInit()
    {
        item = DataManager.itemDB[itemList[Random.Range(0, itemList.Count)]];
        item.rarityType = rarityType;
        item.stat = Random.Range(item.statMin, item.statMax);
    }

    private static void Additional()
    {
        rarityAdd = DataManager.Find(DataManager.rarity, "Function", "rarityAdd");
        int rand = Random.Range(0, 100);
        int rarityAll = item.rarity + (int)rarityAdd[rarityType];
        if (rand > 25) Mathf.Max(1, rarityAll - 1);
        for (int i = 0; i < DataManager.additional.Count; i++)
        {
            if (rarityAll == (int)DataManager.additional[i]["rarity"])
                additionalList.Add(DataManager.additional[i]["code"].ToString());
        }
        for (int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, additionalList.Count);
            Dictionary<string, object> additional = DataManager.Find(DataManager.additional, "code", additionalList[rand]);
            item.nameAdd[i] = additional["Name"].ToString();
            item.statusAdd[i] = additional["status"].ToString();
            item.statAdd[i] = Random.Range((int)additional["statMin"], (int)additional["statMax"]);
            item.cost += (int)additional["cost"];
        }
    }
}

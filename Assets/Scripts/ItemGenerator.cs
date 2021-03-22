using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    private static ItemGenerator instance;
    public static ItemGenerator Instance
    {
        get
        {
            if (instance == null) return null;
            return instance;
        }
    }
    [SerializeField]
    private Item item;
    private Dictionary<string, object> rarity;
    private Dictionary<string, object> rarityAdd;
    private List<string> itemList = new List<string>();
    private List<string> additionalList = new List<string>();
    private string rarityType = "Normal";
    [SerializeField]
    private GameObject itemPrefab;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void DropItem(int rarityMin, int rarityMax, string type, Vector3 pos)
    {
        Filtering(rarityMin, rarityMax);
        RandomRarity(type);
        ItemInit();
        Additional();
        GameObject clone = Instantiate(itemPrefab, pos, Quaternion.identity);
        clone.GetComponent<ItemScript>().item = item;
    }

    private void Filtering(int rarityMin, int rarityMax)
    {
        for (int i = 0; i < DataManager.item.Count; i++)
        {
            if (rarityMin <= (int)DataManager.item[i]["rarity"] &&
                rarityMax >= (int)DataManager.item[i]["rarity"])
                itemList.Add(DataManager.item[i]["name"].ToString());
        }
    }

    private void RandomRarity(string type)
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

    private void ItemInit()
    {
        item = DataManager.itemDB[itemList[Random.Range(0, itemList.Count)]];
        item.rarityType = rarityType;
        item.stat = Random.Range(item.statMin, item.statMax);
    }

    private void Additional()
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

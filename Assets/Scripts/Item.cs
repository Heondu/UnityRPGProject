﻿public class Item
{
    private const int additionalMax = 3;

    public string name;
    public int spawnable;
    public string useType;
    public string type;
    public string status;
    public int rarity;
    public int statMin;
    public int statMax;
    public int cost;
    public int stat;
    public string rarityType;
    public string[] nameAdd = new string[additionalMax];
    public string[] statusAdd = new string[additionalMax];
    public int[] statAdd = new int[additionalMax];
    public string itemImage;
    public string inventoryImage;
}

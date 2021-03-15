using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private Item weapon;
    [SerializeField]
    private Item armor;
    [SerializeField]
    private Item accessories;
    [SerializeField]
    private List<Item> itemList = new List<Item>();

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Equip(Item item)
    {
        if (DataManager.Exists(DataManager.weapon, "name", item.name))
            weapon = item;
        if (DataManager.Exists(DataManager.armor, "name", item.name))
            armor = item;
        if (DataManager.Exists(DataManager.accessories, "name", item.name))
            accessories = item;

        StatusCalc();
    }

    public void StatusCalc()
    {
        Item[] items = { weapon, armor, accessories };
        player.status.StatusCalc(items);
    }
}

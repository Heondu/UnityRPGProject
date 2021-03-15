using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private Item weapon;
    [SerializeField]
    private Item armor;
    [SerializeField]
    private Item accessories;
    [SerializeField]
    private List<Item> itemList = new List<Item>(); 

    [ContextMenu("Equip")]
    private void Equip()
    {
        if (DataManager.Exists(DataManager.weapon, "name", weapon.name))
            weapon = DataManager.itemDB[weapon.name];
        if (DataManager.Exists(DataManager.armor, "name", armor.name))
            armor = DataManager.itemDB[armor.name];
        if (DataManager.Exists(DataManager.accessories, "name", accessories.name))
            accessories = DataManager.itemDB[accessories.name];
    }
}

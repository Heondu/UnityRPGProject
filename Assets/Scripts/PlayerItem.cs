﻿using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        InventoryManager.instance.onItemEquipCallback += Equip;
        InventoryManager.instance.onItemUnequipCallback += Unequip;
    }

    public void PickUp(Item item)
    {
        InventoryManager.instance.AddItem(item);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemScript item = collision.GetComponent<ItemScript>();
        if (item != null)
        {
            PickUp(item.item);
            Destroy(collision.gameObject);
        }
    }

    public void Equip(Item item)
    {
        Status status = player.status.GetStatus(item.status);
        if (item.status.Contains("%")) status.AddModifier(new StatusModifier(item.stat, StatusModType.PercentAdd, item));
        else status.AddModifier(new StatusModifier(item.stat, StatusModType.Flat, item));
        for (int i = 0; i < item.statusAdd.Length; i++)
        {
            status = player.status.GetStatus(item.statusAdd[i]);
            if (item.statusAdd[i].Contains("%")) status.AddModifier(new StatusModifier(item.statAdd[i], StatusModType.PercentAdd, item));
            else status.AddModifier(new StatusModifier(item.statAdd[i], StatusModType.Flat, item));
        }
    }

    public void Unequip(Item item)
    {
        Status status = player.status.GetStatus(item.status);
        status.RemoveAllModifiersFromSource(item);
        for (int i = 0; i < item.statusAdd.Length; i++)
        {
            status = player.status.GetStatus(item.statusAdd[i]);
            status.RemoveAllModifiersFromSource(item);
        }
    }
}

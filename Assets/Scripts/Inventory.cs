using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Slot[] itemSlots;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        itemSlots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < transform.childCount; i++)
            items.Add(null);
    }

    public void AddItem(Item newItem)
    {
        int index;
        if (newItem.useType == "equipment")
        {
            index = FindItem(itemSlots, null);
            if (index != -1) items[index] = newItem;
        }
        else if (newItem.useType == "consume")
        {
            index = FindItem(itemSlots, newItem);
            if (index != -1) items[index].quantity++;
            else
            {
                index = FindItem(itemSlots, null);
                items[index] = newItem;
            }
        }
        UpdateInventory();
    }

    public void ChangeItem(Item changedItem, int index)
    {
        items[index] = changedItem;
        UpdateInventory();
    }

    public void RemoveItem(Item targetItem)
    {
        int index = FindItem(itemSlots, targetItem);
        if (index != -1) items[index] = null;
        UpdateInventory();
    }

    public int FindItem(Slot[] itemSlots, Item newItem)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == newItem) return i;
        }
        return -1;
    }

    private int FindIndex(Transform transform)
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform) return i;
        }
        return -1;
    }

    private void FindSlot(Slot selectedSlot, Slot targetSlot)
    {
        Item selectedItem = selectedSlot.item;
        Item targetItem = targetSlot.item;
        Inventory selectedInventory = selectedSlot.transform.parent.GetComponent<Inventory>();
        Inventory targetInventory = targetSlot.transform.parent.GetComponent<Inventory>();
        targetInventory.ChangeItem(selectedItem, FindIndex(targetSlot.transform));
        selectedInventory.ChangeItem(targetItem, FindIndex(selectedSlot.transform));
    }

    public void IsChangable(Slot selectedSlot, Slot targetSlot)
    {
        if (selectedSlot.item.useType == targetSlot.useType)
        {
            if (selectedSlot.item.useType == "equipment")
            {
                for (int i = 0; i < targetSlot.equipType.Length; i++)
                {
                    if (targetSlot.equipType[i] == "all" || selectedSlot.item.type == targetSlot.equipType[i])
                    {
                        FindSlot(selectedSlot, targetSlot);
                        break;
                    }
                    else
                    {
                        selectedSlot.icon.color = Color.white;
                    }
                }
            }
            else if (selectedSlot.item.useType == "consume")
            {
                FindSlot(selectedSlot, targetSlot);
            }
        }
        else
        {
            selectedSlot.icon.color = Color.white;
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemSlots[i].item = items[i];
        }

        InventoryManager.instance.onItemChangedCallback.Invoke();
    }

    public void QuickEquip(Slot selectedSlot)
    {
        if (selectedSlot.item.useType == "equipment")
        {
            if (selectedSlot.CompareTag("InventoryEquip"))
            {
                Inventory inventory = InventoryManager.instance.GetInventory(InventoryUseType.equipment);
                int index = FindItem(inventory.itemSlots, null);
                if (index != -1) FindSlot(selectedSlot, inventory.itemSlots[index]);
            }
            else
            {
                Inventory inventory = InventoryManager.instance.GetInventory(InventoryUseType.equipSlot);
                for (int i = 0; i < inventory.itemSlots.Length; i++)
                {
                    for (int j = 0; j < inventory.itemSlots[i].equipType.Length; j++)
                    {
                        if (selectedSlot.item.type == inventory.itemSlots[i].equipType[j])
                        {
                            FindSlot(selectedSlot, inventory.itemSlots[i]);
                            return;
                        }
                    }
                }
            }
        }
    }
    //public void Disable()
    //{
    //    popupUI.ClosePopup();
    //    draggingItem.SetActive(false);
    //    if (selectedItem != null) selectedItem.icon.color = Color.white;
    //}
}

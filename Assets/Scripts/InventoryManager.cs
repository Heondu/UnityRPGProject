using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryUseType { equipment = 0, consume, equipSlot = 5 }

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField]
    private Inventory[] inventorys;
    [SerializeField]
    private GameObject draggingItem;
    private Slot draggingSlot;
    private Item selectedItem;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        draggingSlot = draggingItem.GetComponent<Slot>();
    }

    public void OnBeginDrag(Item selectedItem)
    {
        draggingSlot.item = selectedItem;
        draggingItem.SetActive(true);
        this.selectedItem = selectedItem;
    }

    public void OnDrag()
    {
        draggingItem.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(Slot selectedSlot, Slot targetSlot)
    {
        draggingSlot.item = null;
        draggingItem.SetActive(false);
        if (targetSlot == null) selectedSlot.icon.color = Color.white;
        else inventorys[FindUseType(selectedSlot.item)].IsChangable(selectedSlot, targetSlot);
    }

    public void RemoveItem()
    {
        draggingSlot.item = null;
        draggingItem.SetActive(false);
    }

    private int FindUseType(Item targetItem)
    {
        if (targetItem.useType == "equipment") return (int)InventoryUseType.equipment;
        else if (targetItem.useType == "consume") return (int)InventoryUseType.consume;
        else return -1;
    }

    public void AddItem(Item newItem)
    {
        int useType = FindUseType(newItem);
        inventorys[useType].AddItem(newItem);
    }

    public Inventory GetInventory(InventoryUseType useType)
    {
        return inventorys[(int)useType];
    }
}

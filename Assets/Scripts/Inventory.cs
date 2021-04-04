using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject[] itemHolders;
    private List<Slot[]> itemSlots = new List<Slot[]>();
    [SerializeField]
    private GameObject equipmentHolder;
    private Slot[] equipSlots;
    public PopupUI popupUI;
    [SerializeField]
    private GameObject draggingItem;
    private PlayerItem playerItem;
    private Slot selectedItem;

    private void Awake()
    {
        for (int i = 0; i < itemHolders.Length; i++)
            itemSlots.Add(itemHolders[i].GetComponentsInChildren<Slot>());
        equipSlots = equipmentHolder.GetComponentsInChildren<Slot>();
        playerItem = FindObjectOfType<PlayerItem>();
    }

    public void OnBeginDrag(Slot selectedItem)
    {
        draggingItem.SetActive(true);
        this.selectedItem = selectedItem;
        draggingItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = selectedItem.itemIcon.sprite;
        if (selectedItem.item.useType == "equipment") selectedItem.itemIcon.color = Color.clear;
    }

    public void OnDrag()
    {
        draggingItem.transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(Slot selectedItem, Slot changedItem, bool isEquip)
    {
        if (draggingItem.activeSelf == false) return;
        if (changedItem == null)
        {
            if (isEquip)
            {
                changedItem = ItemToInventorySlot(selectedItem.item, out bool isEmpty);
                if (isEmpty) ChangeItemSlot(selectedItem, changedItem);
                else selectedItem.itemIcon.color = Color.white;
                popupUI.ClosePopup();
            }
            else if (selectedItem.item.useType == "consume") ItemShortcutAssign(selectedItem, changedItem);
            else selectedItem.itemIcon.color = Color.white;
        }
        else if (changedItem == selectedItem) selectedItem.itemIcon.color = Color.white;
        else if (selectedItem.item.useType != changedItem.useType) selectedItem.itemIcon.color = Color.white;
        else if (changedItem.isLock) selectedItem.itemIcon.color = Color.white;
        else if (changedItem.useType == "consume") ItemShortcutAssign(selectedItem, changedItem);
        else if (changedItem.useType == "equipment")
        {
            foreach (string type in changedItem.equipType)
            {
                if (type == "all" || type == selectedItem.item.type)
                {
                    ChangeItemSlot(selectedItem, changedItem);
                    playerItem.Equip(equipSlots);
                    popupUI.UpdatePopup(changedItem);
                    break;
                }
                else selectedItem.itemIcon.color = Color.white;
            }
        }
        else selectedItem.itemIcon.color = Color.white;
        draggingItem.SetActive(false);
    }

    private void ChangeItemSlot(Slot selectedItem, Slot changedItem)
    {
        Item temp = changedItem.item;
        changedItem.item = selectedItem.item;
        selectedItem.item = temp;
        Sprite spriteTemp = changedItem.itemIcon.sprite;
        Color colorTemp = changedItem.itemIcon.color;
        int quantityTemp = changedItem.quantity;
        string skillTemp = changedItem.skill;
        changedItem.itemIcon.sprite = selectedItem.itemIcon.sprite;
        changedItem.quantity = selectedItem.quantity;
        changedItem.itemIcon.color = Color.white;
        changedItem.skill = selectedItem.skill;
        selectedItem.itemIcon.sprite = spriteTemp;
        selectedItem.itemIcon.color = colorTemp;
        selectedItem.quantity = quantityTemp;
        selectedItem.skill = skillTemp;
    }

    private void ItemShortcutAssign(Slot selectedItem, Slot changedItem)
    {
        Shortcut selectedShortcut = null;
        Shortcut changedShortcut = null;
        if (selectedItem != null) selectedShortcut = selectedItem.GetComponent<Shortcut>();
        if (changedItem != null) changedShortcut = changedItem.GetComponent<Shortcut>();
        if (selectedShortcut == null && changedItem == null) return;
        else if ((selectedShortcut != null && changedItem == null) || (selectedShortcut != null && changedShortcut == null))
        {
            selectedItem.item = null;
            selectedItem.itemIcon.color = Color.clear;
            selectedItem.quantity = 0;
            selectedItem.skill = null;
        }
        else if (selectedShortcut == null && changedShortcut != null)
        {
            changedItem.item = selectedItem.item;
            changedItem.itemIcon.sprite = selectedItem.itemIcon.sprite;
            changedItem.itemIcon.color = Color.white;
            changedItem.quantity = selectedItem.quantity;
            changedItem.skill = selectedItem.skill;
        }
        else ChangeItemSlot(selectedItem, changedItem);
        if (selectedShortcut != null) selectedShortcut.ShortcutAssign(selectedItem);
        if (changedShortcut != null) changedShortcut.ShortcutAssign(changedItem);
    }

    public void Equip(Slot selectedItem, bool isEquip)
    {
        if (selectedItem.item == null) return;
        if (isEquip)
        {
            Slot changedItem = ItemToInventorySlot(selectedItem.item, out bool isEmpty);
            if (isEmpty)
            {
                ChangeItemSlot(selectedItem, changedItem);
                popupUI.ClosePopup();
            }
        }
        else
        {
            Slot ChangedItem = null;
            bool flag = false;
            for (int i = 0; i < equipSlots.Length; i++)
            {
                for (int j = 0; j < equipSlots[i].equipType.Length; j++)
                {
                    if (selectedItem.item.useType != equipSlots[i].useType) continue;
                    if (selectedItem.item.type != equipSlots[i].equipType[j]) continue;
                    if (equipSlots[i].isLock) continue;
                    if (ChangedItem == null) ChangedItem = equipSlots[i];
                    if (equipSlots[i].item == null)
                    {
                        ChangedItem = equipSlots[i];
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }
            if (ChangedItem == null) return;
            ChangeItemSlot(selectedItem, ChangedItem);
            playerItem.Equip(equipSlots);
            if (selectedItem.item == null) popupUI.ClosePopup();
            else popupUI.UpdatePopup(selectedItem);
        }
    }

    public Slot ItemToInventorySlot(Item item, out bool isEmpty)
    {
        int num = 0;
        if (item.useType == "equipment") num = 0;
        else if (item.useType == "consume") num = 1;

        for (int i = 0; i < itemSlots[num].Length; i++)
        {
            if (item.useType == "equipment" && itemSlots[num][i].item == null)
            {
                isEmpty = true;
                return itemSlots[num][i];
            }
            else if (item.useType == "consume")
            {
                if (itemSlots[num][i].item == item)
                {
                    itemSlots[num][i].quantity++;
                    isEmpty = false;
                    return null;
                }
                else if (itemSlots[num][i].item == null)
                {
                    itemSlots[num][i].quantity++;
                    isEmpty = true;
                    return itemSlots[num][i];
                }
            }
        }
        isEmpty = false;
        return null;
    }

    public void DestroyItem(Slot selectedItem)
    {
        selectedItem.item = null;
        selectedItem.itemIcon.color = Color.clear;
        selectedItem.quantity = 0;
        selectedItem.skill = null;
        draggingItem.SetActive(false);
    }

    public void Disable()
    {
        popupUI.ClosePopup();
        draggingItem.SetActive(false);
        if (selectedItem != null) selectedItem.itemIcon.color = Color.white;
    }
}

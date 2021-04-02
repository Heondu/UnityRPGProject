using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject itemHolder;
    private Slot[] itemSlots;
    [SerializeField]
    private GameObject equipmentHolder;
    private Slot[] equipSlots;
    [SerializeField]
    private GameObject itemPopup;
    private RectTransform itemPopupRect;
    [SerializeField]
    private GameObject draggingItem;
    private PlayerItem playerItem;
    private Slot selectedItem;

    private void Awake()
    {
        itemSlots = itemHolder.GetComponentsInChildren<Slot>();
        equipSlots = equipmentHolder.GetComponentsInChildren<Slot>();
        playerItem = FindObjectOfType<PlayerItem>();
        itemPopupRect = itemPopup.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (itemPopup.activeSelf) PopupFollowMouse();
    }

    public void OnBeginDrag(Slot selectedItem)
    {
        draggingItem.SetActive(true);
        this.selectedItem = selectedItem;
        draggingItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = selectedItem.itemIcon.sprite;
        selectedItem.itemIcon.color = Color.clear;
    }

    public void OnDrag()
    {
        draggingItem.transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(Slot changedItem, bool isEquip)
    {
        if (draggingItem.activeSelf == false) return;
        if (changedItem == null)
        {
            if (isEquip)
            {
                changedItem = ItemToInventorySlot();
                if (changedItem != null) ChangeItemSlot(selectedItem, changedItem);
                ClosePopupItem();
            }
            else selectedItem.itemIcon.color = Color.white;
        }
        else if (changedItem == selectedItem) selectedItem.itemIcon.color = Color.white;
        else
        {
            foreach (string type in changedItem.equipType)
            {
                if (type == "all" || type == selectedItem.item.type)
                {
                    if (changedItem.isLock == false)
                    {
                        ChangeItemSlot(selectedItem, changedItem);
                        playerItem.Equip();
                        PopupItem(changedItem);
                        break;
                    }
                    else selectedItem.itemIcon.color = Color.white;
                }
            }
        }
        draggingItem.SetActive(false);
    }

    private void ChangeItemSlot(Slot selectedItem, Slot changedItem)
    {
        Item temp = changedItem.item;
        changedItem.item = selectedItem.item;
        selectedItem.item = temp;
        Sprite spriteTemp = changedItem.itemIcon.sprite;
        Color colorTemp = changedItem.itemIcon.color;
        changedItem.itemIcon.sprite = selectedItem.itemIcon.sprite;
        changedItem.itemIcon.color = Color.white;
        selectedItem.itemIcon.sprite = spriteTemp;
        selectedItem.itemIcon.color = colorTemp;
    }

    public void Equip(Slot selectedItem, bool isEquip)
    {
        if (selectedItem.item == null) return;
        if (isEquip)
        {
            Slot changedItem = ItemToInventorySlot();
            if (changedItem != null) ChangeItemSlot(selectedItem, changedItem);
            ClosePopupItem();
        }
        else
        {
            Slot tempSlot = null;
            bool flag = false;
            for (int i = 0; i < equipSlots.Length; i++)
            {
                for (int j = 0; j < equipSlots[i].equipType.Length; j++)
                {
                    if (selectedItem.item.type != equipSlots[i].equipType[j]) continue;
                    if (equipSlots[i].isLock) continue;
                    if (tempSlot == null) tempSlot = equipSlots[i];
                    if (equipSlots[i].item == null)
                    {
                        tempSlot = equipSlots[i];
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }
            if (tempSlot == null) return;
            ChangeItemSlot(selectedItem, tempSlot);
            playerItem.Equip();
            if (selectedItem.item == null) ClosePopupItem();
            else PopupItem(selectedItem);
        }
    }

    public Slot ItemToInventorySlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                return itemSlots[i];
            }
        }
        return null;
    }

    public void DestroyItem(Slot selectedItem)
    {
        selectedItem.item = null;
        selectedItem.itemIcon.color = Color.clear;
        draggingItem.SetActive(false);
    }

    public void PopupItem(Slot selectedItem)
    {
        if (draggingItem.activeSelf) return;
        itemPopup.SetActive(true);
        itemPopup.transform.Find("ItemIcon").GetComponent<Image>().sprite = selectedItem.itemIcon.sprite;
        itemPopup.transform.Find("Name").GetComponent<Text>().text = $"{DataManager.Localization(selectedItem.item.nameAdd[0])} {DataManager.Localization(selectedItem.item.name)}";
        itemPopup.transform.Find("QualityName").GetComponent<Text>().text = $"{DataManager.Localization(selectedItem.item.rarityType)} {DataManager.Localization(selectedItem.item.type)}";
        itemPopup.transform.Find("BaseStatusTitle").GetComponent<Text>().text = DataManager.Localization(selectedItem.item.status);
        itemPopup.transform.Find("BaseStatusTitle").Find("Status").GetComponent<Text>().text = selectedItem.item.stat.ToString();
        itemPopup.transform.Find("StatusTitle (1)").GetComponent<Text>().text = DataManager.Localization(selectedItem.item.statusAdd[0]);
        itemPopup.transform.Find("StatusTitle (1)").Find("Status").GetComponent<Text>().text = selectedItem.item.statAdd[0].ToString();
        itemPopup.transform.Find("StatusTitle (2)").GetComponent<Text>().text = DataManager.Localization(selectedItem.item.statusAdd[1]);
        itemPopup.transform.Find("StatusTitle (2)").Find("Status").GetComponent<Text>().text = selectedItem.item.statAdd[1].ToString();
        itemPopup.transform.Find("StatusTitle (3)").GetComponent<Text>().text = DataManager.Localization(selectedItem.item.statusAdd[2]);
        itemPopup.transform.Find("StatusTitle (3)").Find("Status").GetComponent<Text>().text = selectedItem.item.statAdd[2].ToString();
    }

    public void ClosePopupItem()
    {
        itemPopup.SetActive(false);
    }

    private void PopupFollowMouse()
    {
        itemPopup.transform.position = Input.mousePosition;
        if (itemPopup.transform.position.y < Screen.height / 2) itemPopupRect.pivot = Vector2.zero;
        else itemPopupRect.pivot = Vector2.up;
        if (itemPopup.transform.position.x + itemPopupRect.sizeDelta.x > Screen.width) itemPopupRect.pivot = new Vector2(1, itemPopupRect.pivot.y);
    }

    public void OnDisable()
    {
        ClosePopupItem();
        draggingItem.SetActive(false);
        if (selectedItem != null) selectedItem.itemIcon.color = Color.white;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject itemPopup;
    [SerializeField]
    private GameObject draggingItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.inventory])) inventory.SetActive(!inventory.activeSelf);
        if (itemPopup.activeSelf) PopupFollowMouse();
    }

    public void OnBeginDrag(InventoryItem selectedItem)
    {
        draggingItem.SetActive(true);
        draggingItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = selectedItem.itemIcon.sprite;
        selectedItem.itemIcon.color = Color.clear;
    }

    public void OnDrag()
    {
        draggingItem.transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(InventoryItem selectedItem, InventoryItem changeItem)
    {
        if (draggingItem.activeSelf == false) return;
        if (changeItem == null || changeItem == selectedItem)
        {
            selectedItem.itemIcon.color = Color.white;
            draggingItem.SetActive(false);
            return;
        }
        foreach (string type in changeItem.equipType)
        {
            if (type == "all" || type == selectedItem.item.type)
            {
                Item temp = changeItem.item;
                changeItem.item = selectedItem.item;
                selectedItem.item = temp;

                Sprite spriteTemp = changeItem.itemIcon.sprite;
                Color colorTemp = changeItem.itemIcon.color;
                changeItem.itemIcon.sprite = selectedItem.itemIcon.sprite;
                changeItem.itemIcon.color = Color.white;
                selectedItem.itemIcon.sprite = spriteTemp;
                selectedItem.itemIcon.color = colorTemp;
                FindObjectOfType<PlayerItem>().Equip();
                draggingItem.SetActive(false);
                return;
            }
        }
        selectedItem.itemIcon.color = Color.white;
        draggingItem.SetActive(false);
    }

    public void PopupItem(InventoryItem selectedItem)
    {
        if (draggingItem.activeSelf) return;
        itemPopup.SetActive(true);
        itemPopup.transform.Find("ItemIcon").GetComponent<Image>().sprite = selectedItem.itemIcon.sprite;
        itemPopup.transform.Find("Name").GetComponent<Text>().text = selectedItem.item.name;
        itemPopup.transform.Find("QualityName").GetComponent<Text>().text = selectedItem.item.rarityType;
        itemPopup.transform.Find("BaseStatusTitle").GetComponent<Text>().text = selectedItem.item.status;
        itemPopup.transform.Find("BaseStatusTitle").Find("Status").GetComponent<Text>().text = selectedItem.item.stat.ToString();
        itemPopup.transform.Find("StatusTitle (1)").GetComponent<Text>().text = selectedItem.item.nameAdd[0];
        itemPopup.transform.Find("StatusTitle (1)").Find("Status").GetComponent<Text>().text = selectedItem.item.statAdd[0].ToString();
        itemPopup.transform.Find("StatusTitle (2)").GetComponent<Text>().text = selectedItem.item.nameAdd[1];
        itemPopup.transform.Find("StatusTitle (2)").Find("Status").GetComponent<Text>().text = selectedItem.item.statAdd[1].ToString();
        itemPopup.transform.Find("StatusTitle (3)").GetComponent<Text>().text = selectedItem.item.nameAdd[2];
        itemPopup.transform.Find("StatusTitle (3)").Find("Status").GetComponent<Text>().text = selectedItem.item.statAdd[2].ToString();
    }

    public void ClosePopupItem()
    {
        itemPopup.SetActive(false);
    }

    private void PopupFollowMouse()
    {
        itemPopup.transform.position = Input.mousePosition;
        if (itemPopup.transform.position.y < Screen.height / 2) itemPopup.GetComponent<RectTransform>().pivot = Vector2.zero;
        else itemPopup.GetComponent<RectTransform>().pivot = Vector2.up;
    }
}

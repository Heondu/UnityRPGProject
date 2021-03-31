using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject itemPopup;
    [SerializeField]
    private GameObject equipment;
    [SerializeField]
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> results = new List<RaycastResult>();
    [SerializeField]
    private GameObject dragObject;
    private InventoryItem selectedItem;

    private void Awake()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.inventory])) inventory.SetActive(!inventory.activeSelf);
        OnMouse();
        IsMouseOnItem();
        ItemDrag();
    }

    private void OnMouse()
    {
        pointerEventData.position = Input.mousePosition;
        results.Clear();
        graphicRaycaster.Raycast(pointerEventData, results);
    }

    private void IsMouseOnItem()
    {
        int count = 0;
        foreach (RaycastResult result in results)
        {
            GameObject hit = result.gameObject;
            if (!hit.CompareTag("InventoryItem")) continue;
            InventoryItem inventoryItem = hit.transform.parent.GetComponent<InventoryItem>();
            if (inventoryItem.item.name == "") continue;
            if (dragObject.activeSelf) continue;
            if (Input.GetMouseButton(0)) ItemDragInit(inventoryItem);
            else
            {
                count++;
                PopupItem(hit, inventoryItem.item);
            }
        }
        if (count == 0) itemPopup.SetActive(false);
    }

    private void ItemDrag()
    {
        if (dragObject.activeSelf == false) return;
        if (Input.GetMouseButton(0)) dragObject.transform.position = pointerEventData.position;
        else
        {
            int count = 0;
            foreach (RaycastResult result in results)
            {
                GameObject hit = result.gameObject;
                if (!hit.CompareTag("InventoryItem")) continue;
                if (hit.transform.parent.gameObject == selectedItem.gameObject) continue;

                InventoryItem inventoryItem = hit.transform.parent.GetComponent<InventoryItem>();
                Item temp = inventoryItem.item;
                inventoryItem.item = selectedItem.item;
                selectedItem.item = temp;

                Sprite spriteTemp = inventoryItem.itemIcon.sprite;
                Color colorTemp = inventoryItem.itemIcon.color;
                inventoryItem.itemIcon.sprite = dragObject.transform.Find("ItemIcon").GetComponent<Image>().sprite;
                inventoryItem.itemIcon.color = Color.white;
                selectedItem.itemIcon.sprite = spriteTemp;
                selectedItem.itemIcon.color = colorTemp;

                if (hit.transform.parent.gameObject.CompareTag("InventoryEquip"))
                    FindObjectOfType<PlayerItem>().Equip(equipment.GetComponentsInChildren<InventoryItem>());

                dragObject.SetActive(false);
                count++;
                break;
            }
            if (count == 0)
            {
                selectedItem.itemIcon.color = Color.white;
                dragObject.SetActive(false);
            }
        }

    }

    private void ItemDragInit(InventoryItem selectedItem)
    {
        dragObject.SetActive(true);
        this.selectedItem = selectedItem;
        dragObject.transform.Find("ItemIcon").GetComponent<Image>().sprite = selectedItem.itemIcon.sprite;
        selectedItem.transform.Find("ItemIcon").GetComponent<Image>().color = Color.clear;
    }

    private void PopupItem(GameObject itemObject, Item item)
    {
        itemPopup.transform.position = pointerEventData.position;
        if (itemPopup.transform.position.y < Screen.height / 2)  itemPopup.GetComponent<RectTransform>().pivot = Vector2.zero;
        else itemPopup.GetComponent<RectTransform>().pivot = Vector2.up;
        itemPopup.SetActive(true);
        itemPopup.transform.Find("ItemIcon").GetComponent<Image>().sprite = itemObject.GetComponent<Image>().sprite;
        itemPopup.transform.Find("Name").GetComponent<Text>().text = item.name;
        itemPopup.transform.Find("QualityName").GetComponent<Text>().text = item.rarityType;
        itemPopup.transform.Find("BaseStatusTitle").GetComponent<Text>().text = item.status;
        itemPopup.transform.Find("BaseStatusTitle").Find("Status").GetComponent<Text>().text = item.stat.ToString();
        itemPopup.transform.Find("StatusTitle (1)").GetComponent<Text>().text = item.nameAdd[0];
        itemPopup.transform.Find("StatusTitle (1)").Find("Status").GetComponent<Text>().text = item.statAdd[0].ToString();
        itemPopup.transform.Find("StatusTitle (2)").GetComponent<Text>().text = item.nameAdd[1];
        itemPopup.transform.Find("StatusTitle (2)").Find("Status").GetComponent<Text>().text = item.statAdd[1].ToString();
        itemPopup.transform.Find("StatusTitle (3)").GetComponent<Text>().text = item.nameAdd[2];
        itemPopup.transform.Find("StatusTitle (3)").Find("Status").GetComponent<Text>().text = item.statAdd[2].ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item = null;
    public Image icon;
    public Image qualty;
    public TextMeshProUGUI quantityText;
    public int quantity = 0;
    private GameObject lockIcon;
    public string useType;
    public string[] equipType;
    public bool isLock;
    public string skill;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        InventoryManager.instance.onItemChangedCallback += UpdateSlot;
        if (transform.Find("ItemIcon") != null) icon = transform.Find("ItemIcon").GetComponent<Image>();
        if (transform.Find("Quality") != null) qualty = transform.Find("Quality").GetComponent<Image>();
        if (transform.Find("Quantity") != null) quantityText = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        if (transform.Find("Isable") != null) lockIcon = transform.Find("Isable").gameObject;
    }

    public void Update()
    {
        if (quantityText == null) return;
        if (item == null || quantity == 0) quantityText.text = "";
        else if (item.useType != "equipment") quantityText.text = quantity.ToString();
    }

    private void OnEnable()
    {
        UpdateSlot();
    }

    private void UpdateSlot()
    {
        if (item == null)
        {
            icon.color = Color.clear;
        }
        else
        {
            icon.sprite = Resources.Load<Sprite>(item.inventoryImage);
            icon.color = Color.white;
            quantity = item.quantity;
        }
    }

    [ContextMenu("Lock")]
    public void Lock()
    {
        isLock = !isLock;
        lockIcon.SetActive(isLock);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLock) return;
        if (item == null) return;
        if (eventData.button == PointerEventData.InputButton.Right) inventory.QuickEquip(this);
        else if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount > 1) inventory.QuickEquip(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            PopupUI.instance.SetActive(true);
            PopupUI.instance.UpdatePopup(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PopupUI.instance.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (item != null)
        {
            InventoryManager.instance.OnBeginDrag(item);
            if (item.useType == "equipment") icon.color = Color.clear;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.instance.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (eventData.pointerEnter.name == "Bin")
        {
            inventory.RemoveItem(item);
            InventoryManager.instance.RemoveItem();
        }
        else InventoryManager.instance.OnEndDrag(this, eventData.pointerEnter.GetComponent<Slot>());
    }
}

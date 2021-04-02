using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item = null;
    public Image itemIcon;
    public Image qualty;
    public Text quantity;
    private GameObject lockIcon;
    private Inventory inventory;
    public string[] equipType;
    public bool isLock;

    private void Awake()
    {
        itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        qualty = transform.Find("Quality").GetComponent<Image>();
        quantity = transform.Find("Quantity").GetComponent<Text>();
        lockIcon = transform.Find("Isable").gameObject;
        inventory = FindObjectOfType<Inventory>();
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
        if (eventData.button == PointerEventData.InputButton.Right)inventory.Equip(this, CompareTag("InventoryEquip"));
        else if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount > 1) inventory.Equip(this, CompareTag("InventoryEquip"));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null) inventory.PopupItem(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.ClosePopupItem();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (item != null) inventory.OnBeginDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLock) return;
        if (eventData.pointerEnter.name == "Bin") inventory.DestroyItem(this);
        else inventory.OnEndDrag(eventData.pointerEnter.GetComponent<Slot>(), CompareTag("InventoryEquip"));
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item = null;
    public Image itemIcon;
    public Image qualty;
    public Text quantity;
    private Inventory inventory;
    public string[] equipType;

    private void Awake()
    {
        itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        qualty = transform.Find("Quality").GetComponent<Image>();
        quantity = transform.Find("Quantity").GetComponent<Text>();
        inventory = FindObjectOfType<Inventory>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item.name != "") inventory.PopupItem(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.ClosePopupItem();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item.name != "") inventory.OnBeginDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventory.OnEndDrag(this, eventData.pointerEnter.GetComponent<InventoryItem>());
    }
}

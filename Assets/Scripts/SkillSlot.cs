using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Skill skill = null;
    public Image icon;
    private GameObject lockIcon;
    public bool isLock;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        //InventoryManager.instance.onItemChangedCallback += UpdateSlot;
        if (transform.Find("Icon") != null) icon = transform.Find("Icon").GetComponent<Image>();
        if (transform.Find("Isable") != null) lockIcon = transform.Find("Isable").gameObject;
    }

    private void OnEnable()
    {
        UpdateSlot();
    }

    private void UpdateSlot()
    {
        if (skill == null)
        {
            icon.color = Color.clear;
        }
        else
        {
            //icon.sprite = Resources.Load<Sprite>(skill.inventoryImage);
            icon.color = Color.white;
        }
    }

    [ContextMenu("Lock")]
    public void Lock()
    {
        isLock = !isLock;
        lockIcon.SetActive(isLock);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLock) return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.instance.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLock) return;
        //else InventoryManager.instance.OnEndDrag(this, eventData.pointerEnter.GetComponent<SkillSlot>());
    }
}

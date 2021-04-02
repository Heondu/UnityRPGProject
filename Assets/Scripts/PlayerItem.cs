using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private Transform equipmentContent;
    private Slot[] equipment;
    private Inventory inventory;
    private Player player;

    private void Awake()
    {
        equipment = equipmentContent.GetComponentsInChildren<Slot>();
        inventory = FindObjectOfType<Inventory>();
        player = GetComponent<Player>();
    }

    public void PickUp(Item item)
    {
        Slot newItem = inventory.ItemToInventorySlot();
        if (newItem == null) return;
        newItem.item = item;
        switch (item.type)
        {
            case "sword": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "axe": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "gun": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "bow": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "fan": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "staff": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "helm": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/hel01"); break;
            case "top": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/arm01"); break;
            case "gloves": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/glv01"); break;
            case "shoes": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "belt": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "ring": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "necklace": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "bracelet": newItem.itemIcon.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
        }
        newItem.itemIcon.color = Color.white;
    }

    public void Equip()
    {
        Item[] items = new Item[equipment.Length];
        for (int i = 0; i < equipment.Length; i++)
        {
            items[i] = equipment[i].item;
        }
        
        StatusCalc(items);
    }

    public void StatusCalc(Item[] items)
    {
        StatusCalculator.StatusCalc(player.status.status, player.playerStatus.fourStatus, items, player.playerSkill.buffs);
    }
}

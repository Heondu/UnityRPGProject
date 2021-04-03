using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private Slot[] hudShortcut;
    [SerializeField]
    private Slot[] inventoryShortcut;
    private Inventory inventory;
    private Player player;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        player = GetComponent<Player>();
    }

    public void PickUp(Item item)
    {
        Slot newItem = inventory.ItemToInventorySlot(item, out bool isEmpty);
        if (isEmpty == false) return;
        newItem.item = item;
        newItem.itemIcon.sprite = Resources.Load<Sprite>(item.inventoryImage);
        newItem.itemIcon.color = Color.white;
    }

    public void Equip(Slot[] equipSlots)
    {
        Item[] items = new Item[equipSlots.Length];
        for (int i = 0; i < equipSlots.Length; i++)
        {
            items[i] = equipSlots[i].item;
        }
        
        StatusCalc(items);
    }

    public void StatusCalc(Item[] items)
    {
        StatusCalculator.StatusCalc(player.status.status, player.playerStatus.fourStatus, items, player.playerSkill.buffs);
    }
}

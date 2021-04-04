using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private Shortcut[] shortcuts;
    private Inventory inventory;
    private Player player;
    private PlayerSkill playerSkill;
    private GameObject buffHolder;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        player = GetComponent<Player>();
        playerSkill = GetComponent<PlayerSkill>();
        buffHolder = GameObject.Find("BuffHolder");
    }

    public void PickUp(Item item)
    {
        Slot newItem = inventory.ItemToInventorySlot(item, out bool isEmpty);
        if (isEmpty == false) return;
        newItem.item = item;
        newItem.itemIcon.sprite = Resources.Load<Sprite>(item.inventoryImage);
        newItem.itemIcon.color = Color.white;
        newItem.skill = item.skill;
    }

    public void Equip(Slot[] equipSlots)
    {
        Item[] items = new Item[equipSlots.Length];
        for (int i = 0; i < equipSlots.Length; i++)
            items[i] = equipSlots[i].item;
        StatusCalc(items);
    }

    public void Execute(int index, GameObject executor)
    {
        if (shortcuts[index].skill == "") return;
        if (DataManager.skillDB[shortcuts[index].skill].isCool) return;
        playerSkill.Execute(DataManager.skillDB[shortcuts[index].skill], executor);
    }

    public void StatusCalc(Item[] items)
    {
        StatusCalculator.StatusCalc(player.status.status, player.playerStatus.baseStatus, items, buffHolder.GetComponentsInChildren<Skill>());
    }
}

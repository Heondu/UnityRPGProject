using UnityEngine;
using System.Reflection;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private Shortcut[] shortcuts;
    private Player player;
    private PlayerInput playerInput;
    private PlayerSkill playerSkill;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        playerSkill = GetComponent<PlayerSkill>();
        InventoryManager.instance.onItemEquipCallback += Equip;
        InventoryManager.instance.onItemUnequipCallback += Unequip;
    }

    private void Update()
    {
        if (IsItemCool(playerInput.GetItemIndex())) Execute(playerInput.GetItemIndex());
    }

    private bool IsItemCool(int index)
    {
        if (index == -1) return false;
        if (shortcuts[index].skill == null) return false;
        if (shortcuts[index].skill.isCool) return false;
        return true;
    }

    public void PickUp(Item item)
    {
        InventoryManager.instance.AddItem(item);
    }

    public void Execute(int index)
    {
        playerSkill.Execute(shortcuts[index].skill);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemScript item = collision.GetComponent<ItemScript>();
        if (item != null)
        {
            PickUp(item.item);
            Destroy(collision.gameObject);
        }
    }

    public void Equip(Item item)
    {
        Status status = player.status.GetStatus(item.status);
        if (item.status.Contains("%")) status.AddModifier(new StatusModifier(item.stat, StatusModType.PercentAdd, item));
        else status.AddModifier(new StatusModifier(item.stat, StatusModType.Flat, item));
        for (int i = 0; i < item.statusAdd.Length; i++)
        {
            status = player.status.GetStatus(item.statusAdd[i]);
            if (item.statusAdd[i].Contains("%")) status.AddModifier(new StatusModifier(item.statAdd[i], StatusModType.PercentAdd, item));
            else status.AddModifier(new StatusModifier(item.statAdd[i], StatusModType.Flat, item));
        }
    }

    public void Unequip(Item item)
    {
        Status status = player.status.GetStatus(item.status);
        status.RemoveAllModifiersFromSource(item);
        for (int i = 0; i < item.statusAdd.Length; i++)
        {
            status = player.status.GetStatus(item.statusAdd[i]);
            status.RemoveAllModifiersFromSource(item);
        }
    }
}

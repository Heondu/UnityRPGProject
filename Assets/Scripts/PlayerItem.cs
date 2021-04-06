using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private Shortcut[] shortcuts;
    private Player player;
    [SerializeField]
    private Transform equipmentHolder;
    private Slot[] equipSlots;
    private PlayerSkill playerSkill;
    private GameObject buffHolder;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerSkill = GetComponent<PlayerSkill>();
        buffHolder = GameObject.Find("BuffHolder");
        equipSlots = equipmentHolder.GetComponentsInChildren<Slot>();
        //InventoryManager.instance.onItemChangedCallback += Equip;
    }

    public void PickUp(Item item)
    {
        InventoryManager.instance.AddItem(item);
    }

    public void Equip()
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
        //StatusCalculator.StatusCalc(player.status.status, player.playerStatus.baseStatus, items);
    }
}

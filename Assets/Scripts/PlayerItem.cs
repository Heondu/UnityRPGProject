using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    private Item[] equipment;
    [SerializeField]
    private Transform invetoryContent;
    private Transform[] inventory;

    private void Awake()
    {
        equipment = new Item[10];
        inventory = new Transform[invetoryContent.childCount];
        for (int i = 0; i < invetoryContent.childCount; i++)
            inventory[i] = invetoryContent.GetChild(i).GetComponent<Transform>();
    }

    public void PickUp(Item item)
    {
        Image itemImage = null;
        for (int i = 0; i < invetoryContent.childCount; i++)
        {
            InventoryItem inventoryItem = inventory[i].GetComponent<InventoryItem>();
            if (inventoryItem.item.name == "") 
            {
                inventoryItem.item = item;
                itemImage = inventory[i].Find("ItemIcon").GetComponent<Image>();
                break;
            }
        }
        switch (item.type)
        {
            case "sword": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "axe": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "gun": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "bow": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "fan": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "staff": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/swd01"); break;
            case "helm": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/hel01"); break;
            case "top": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/arm01"); break;
            case "gloves": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/glv01"); break;
            case "shoes": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "belt": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "ring": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "necklace": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
            case "bracelet": itemImage.sprite = Resources.Load<Sprite>("Interface/EquipIcon/blt01"); break;
        }
        itemImage.color = Color.white;
    }

    public void Equip(InventoryItem[] equipment)
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            this.equipment[i] = equipment[i].item;
        }
        
        StatusCalc();
    }

    public void StatusCalc()
    {
        StatusCalculator.StatusCalc(GetComponent<Player>().status.status, GetComponent<PlayerStatus>().fourStatus, equipment, GetComponent<PlayerSkill>().buffs);
    }
}

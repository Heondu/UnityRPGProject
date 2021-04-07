using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    public Slot[] skillSlots;
    public List<Skill> skills = new List<Skill>();

    protected virtual void Awake()
    {
        skillSlots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < transform.childCount; i++)
            skills.Add(null);
    }

    public virtual void AddSkill(Skill newSkill)
    {
        int index;
        index = FindSkill(skillSlots, null);
        if (index != -1) skills[index] = newSkill;
        UpdateInventory();
    }

    public virtual void ChangeSkill(Skill changedSkill, int index)
    {
        skills[index] = changedSkill;
        UpdateInventory();
    }

    public virtual int FindSkill(Slot[] skillSlots, Skill newSkill)
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (skillSlots[i].skill == newSkill) return i;
        }
        return -1;
    }

    protected virtual int FindIndex(Transform transform)
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform) return i;
        }
        return -1;
    }

    protected virtual void FindSlot(Slot selectedSlot, Slot targetSlot)
    {
        Item selectedItem = selectedSlot.item;
        Item targetItem = targetSlot.item;
        Inventory selectedInventory = selectedSlot.transform.parent.GetComponent<Inventory>();
        Inventory targetInventory = targetSlot.transform.parent.GetComponent<Inventory>();
        targetInventory.ChangeItem(selectedItem, FindIndex(targetSlot.transform));
        selectedInventory.ChangeItem(targetItem, FindIndex(selectedSlot.transform));
    }

    public virtual void IsChangable(Slot selectedSlot, Slot targetSlot)
    {
        if (selectedSlot.item.useType == targetSlot.useType)
        {
            if (selectedSlot.item.useType == "skill")
            {
                FindSlot(selectedSlot, targetSlot);
            }
            else
            {
                selectedSlot.icon.color = Color.white;
            }
        }
        else
        {
            selectedSlot.icon.color = Color.white;
        }
    }

    public virtual void UpdateInventory()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            skillSlots[i].skill = skills[i];
        }

        InventoryManager.instance.onItemChangedCallback.Invoke();
    }
}

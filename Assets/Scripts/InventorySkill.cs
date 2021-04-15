using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);

        UpdateInventory();
    }

    public void ShortcutAssign(Skill skill)
    {

    }

    public void UpdateInventory()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            
        }
    }
}

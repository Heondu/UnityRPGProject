using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    public Status status = new Status(0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    public Item item = null;
    public int[] changeItems = new int[4];
    public Skill[] skills = new Skill[4];
    public int[] changeSkills = new int[4];

    public void TakeDamage(float damage)
    {
        Debug.Log($"{name} : {damage}");
    }

    public Status GetStatus()
    {
        return status;
    }


    [ContextMenu("Equip")]
    public void Equip(int index)
    {
        if (item != null)
            Status.StatusCalc(status, item.status, false);
        item = ItemDatabase.instance.itemDB[changeItems[index]];
        Status.StatusCalc(status, item.status, true);
    }

    [ContextMenu("Unequip")]
    public void Unequip(int index)
    {
        if (item != null)
            Status.StatusCalc(status, item.status, false);
        item = null;
    }

    [ContextMenu("ChangeSkill")]
    public void ChangeSkill()
    {
        for (int i = 0; i < 4; i++)
        {
            skills[i] = SkillDatabase.instance.skillDB[changeSkills[i]];
        }
    }
}

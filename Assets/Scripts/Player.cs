using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    public Status status = new Status(0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    public Item item = null;
    public int changeItemNum;
    public Skill skill = null;
    public int changeSkill;

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }

    [ContextMenu("Equip")]
    public void Equip()
    {
        if (item != null)
            Status.StatusCalc(status, item.status, false);
        item = ItemDatabase.instance.itemDB[changeItemNum];
        Status.StatusCalc(status, item.status, true);
    }

    [ContextMenu("Unequip")]
    public void Unequip()
    {
        if (item != null)
            Status.StatusCalc(status, item.status, false);
        item = null;
    }

    [ContextMenu("ChangeSkill")]
    public void ChangeSkill()
    {
        skill = SkillDatabase.instance.skillDB[changeSkill];
    }
}

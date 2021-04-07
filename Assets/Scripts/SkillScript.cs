using UnityEngine;

public class SkillScript : MonoBehaviour
{
    [SerializeField]
    protected string[] nextSkills;
    public delegate void Callback(Skill skill);
    protected Callback callback = null;
    protected Timer timer = new Timer();
    protected GameObject executor;
    protected Skill skill;

    protected virtual void Update()
    {

    }

    public virtual void Execute(GameObject executor, Skill skill)
    {
        this.executor = executor;
        this.skill = skill;
    }

    public void SetCallBack(Callback callback)
    {
        this.callback = callback;
    }

    public void CalcSkillStatus(ILivingEntity entity)
    {
        for (int i = 0; i < 2; i++)
        {
            int value;
            Status relatedStatus = entity.GetStatus(skill.relatedStatus[i]);
            Status status = entity.GetStatus(skill.status[i]);
            if (skill.relatedStatus[i] == "") continue;
            if (skill.status[i] == "none") continue;
            else if (skill.relatedStatus[i] == "none") value = skill.amount[i];
            else value = Mathf.RoundToInt(relatedStatus.Value * ((float)skill.amount[i] / 100));

            if (skill.isPositive == 1)
            {
                Debug.Log($"{skill.status[i]}, {status.Value}");
                if (skill.status[i] == "HP") entity.Restore(value);
                else status.AddModifier(new StatusModifier(value, StatusModType.Flat, skill));
                Debug.Log($"{skill.status[i]}, {status.Value}");
            }
            else if (skill.isPositive == 0)
            {
                if (skill.status[i] == "HP") entity.TakeDamage(value);
                else status.AddModifier(new StatusModifier(-value, StatusModType.Flat, skill));
            }
        }
    }
}

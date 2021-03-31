using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private string[] skillnames = new string[5];
    public Skill[] skills = new Skill[5];
    public List<Skill> buffs = new List<Skill>();
    private Timer[] skillCoolTimer = new Timer[5];
    public Timer[] SkillCoolTimer => skillCoolTimer;
    private bool[] isSkillCool = new bool[5];
    public bool[] IsSkillCool => isSkillCool;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            skills[i] = new Skill();
            skills[i].skill = "";
            skillCoolTimer[i] = new Timer();
            isSkillCool[i] = false;
        }

        ChangeSkill();
    }

    private void Update()
    {
        Cooltime();
    }

    [ContextMenu("ChangeSkill")]
    private void ChangeSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (DataManager.Exists(DataManager.skills, "name", skillnames[i]))
            {
                skills[i] = DataManager.skillDB[skillnames[i]];
            }
        }
    }

    public void Execute(int index, GameObject executor)
    {
        SkillScript skill = SkillLoader.SkillLoad(executor, skills[index], transform.position);
        isSkillCool[index] = true;
        if (skills[index].type == "buff")
        {
            buffs.Add(skills[index]);
            skill.SetCallBack(RemoveBuff);
        }
    }

    private void Cooltime()
    {
        for (int i = 0; i < isSkillCool.Length; i++)
        {
            if (!isSkillCool[i]) continue;
            if (skillCoolTimer[i].IsTimeOut(skills[i].cooltime)) isSkillCool[i] = false;
        }
    }

    private void RemoveBuff(Skill skill)
    {
        buffs.Remove(skill);
    }
}

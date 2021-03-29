using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private string[] skillnames = new string[3];
    public Skill[] skills = new Skill[3];
    public List<Skill> buffs = new List<Skill>();
    public Timer[] skillCoolTimer = { new Timer(), new Timer(), new Timer() };
    public bool[] isSkillCool = { false, false, false };

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            skills[i] = new Skill();
            skills[i].skill = "";
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
            if (isSkillCool[i])
            {
                if (skillCoolTimer[i].IsTimeOut(skills[i].cooltime))
                {
                    isSkillCool[i] = false;
                }
            }
        }
    }

    private void RemoveBuff(Skill skill)
    {
        buffs.Remove(skill);
    }
}

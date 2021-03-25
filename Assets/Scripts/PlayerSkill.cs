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
    private Timer timer = new Timer();

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
                PrintSkill(i);
            }
        }
    }

    private void PrintSkill(int index)
    {
        foreach (string key in skills[index].status.Keys)
        {
            Debug.Log($"[{skills[index].status["skill"]}] {key} : {skills[index].status[key]}");
        }
    }

    public void Execute(int index, GameObject executor)
    {
        SkillScript skill = SkillSpawner.SkillSpawn(executor, skills[index], transform.position);
        isSkillCool[index] = true;
        if (skills[index].status["type"].ToString() == "buff")
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
                if (skillCoolTimer[i].IsTimeOut(float.Parse(skills[i].status["cooltime"].ToString())))
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

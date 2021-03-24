using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private string[] skillnames = new string[3];
    public Skill[] skills = new Skill[3];
    [SerializeField]
    private GameObject skillProjectile;
    [SerializeField]
    private GameObject skillExplode;
    [SerializeField]
    private GameObject skillBuff;
    public List<Skill> buffs = new List<Skill>();

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
        if (!DataManager.Exists(DataManager.skills, "name", skillnames[index])) return;

        if (index == 0)
        {
            GameObject clone = Instantiate(skillProjectile, transform.position, Quaternion.identity);
            clone.GetComponent<SkillProjectile>().Execute(Rotation.GetAngle(executor.transform.position), executor, skills[index]);
        }
        else if (index == 1)
        {
            GameObject clone = Instantiate(skillExplode, transform.position, Quaternion.identity);
            clone.GetComponent<SkillExplode>().Execute(executor.transform.position, executor, skills[index]);
        }
        else if (index == 2)
        {
            buffs.Add(skills[index]);
            GameObject clone = Instantiate(skillBuff, Vector3.zero, Quaternion.identity);
            clone.GetComponent<SkillBuff>().Execute(executor, skills[index]);
            clone.GetComponent<SkillBuff>().SetCallBack(RemoveBuff);
        }
    }

    private void RemoveBuff(Skill skill)
    {
        Debug.Log(skill.status["name"]);
        for (int i = 0; i < buffs.Count; i++)
        {
            Debug.Log(buffs[i]);
        }
        buffs.Remove(skill);
        for (int i = 0; i < buffs.Count; i++)
        {
            Debug.Log(buffs[i]);
        }
    }
}

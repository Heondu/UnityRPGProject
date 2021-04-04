using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Shortcut[] shortcuts;

    public void Execute(int index, GameObject executor)
    {
        if (shortcuts[index].skill == "") return;
        if (DataManager.skillDB[shortcuts[index].skill].isCool) return;
        SkillLoader.SkillLoad(executor, DataManager.skillDB[shortcuts[index].skill], transform.position);
        DataManager.skillDB[shortcuts[index].skill].isCool = true;
        StartCoroutine("Cooltime", DataManager.skillDB[shortcuts[index].skill]);
    }

    public void Execute(Skill skill, GameObject executor)
    {
        SkillLoader.SkillLoad(executor, skill, transform.position);
        skill.isCool = true;
        StartCoroutine("Cooltime", skill);
    }

    private IEnumerator Cooltime(Skill skill)
    {
        skill.currentTime = 0;
        while (skill.currentTime < skill.cooltime)
        {
            skill.currentTime += Time.deltaTime;
            yield return null;
        }
        skill.isCool = false;
    }
}

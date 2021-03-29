using UnityEngine;

public class SkillLoader : MonoBehaviour
{
    public static SkillScript SkillLoad(GameObject executor, Skill skill, Vector3 pos)
    {
        if (skill.position == "pointer") pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        GameObject clone = Instantiate(Resources.Load("Prefabs/Skills/" + skill.name) as GameObject, pos, Quaternion.identity);
        SkillScript skillClone = clone.GetComponent<SkillScript>();
        skillClone.Execute(executor, skill);

        return skillClone;
    }
}

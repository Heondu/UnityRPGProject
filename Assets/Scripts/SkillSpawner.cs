using UnityEngine;

public class SkillSpawner : MonoBehaviour
{
    private static GameObject skillProjectile;
    private static GameObject skillExplode;
    private static GameObject skillBuff;

    private void Awake()
    {
        skillProjectile = Resources.Load("Prefabs/Projectile") as GameObject;
        skillExplode = Resources.Load("Prefabs/Explode") as GameObject;
        skillBuff = Resources.Load("Prefabs/Buff") as GameObject;
    }

    public static SkillScript SkillSpawn(GameObject executor, Skill skill, Vector3 pos)
    {
        if (skill.status["position"].ToString() == "pointer") pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject clone = null;
        if (skill.status["type"].ToString() == "projectile") clone = Instantiate(skillProjectile, pos, Quaternion.identity);
        else if (skill.status["type"].ToString() == "explode") clone = Instantiate(skillExplode, pos, Quaternion.identity);
        else if (skill.status["type"].ToString() == "buff") clone = Instantiate(skillBuff, pos, Quaternion.identity);
        SkillScript skillClone = clone.GetComponent<SkillScript>();
        skillClone.Execute(executor, skill);

        return skillClone;
    }
}

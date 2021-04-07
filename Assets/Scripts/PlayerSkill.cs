using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Shortcut[] shortcuts;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (IsAttack(playerInput.GetSkillIndex())) Execute(shortcuts[playerInput.GetSkillIndex()].skill);
    }

    private bool IsAttack(int index)
    {
        if (index == -1) return false;
        if (shortcuts[index].skill == null) return false;
        if (shortcuts[index].skill.isCool) return false;
        return true;
    }

    public void Execute(Skill skill)
    {
        SkillLoader.SkillLoad(gameObject, skill, transform.position);
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

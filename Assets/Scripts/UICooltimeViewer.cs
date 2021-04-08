using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooltimeViewer : MonoBehaviour
{
    [SerializeField]
    private Shortcut shortcut;
    private PlayerSkill playerSkill;
    private Image cooltimeImage;

    private void Awake()
    {
        playerSkill = FindObjectOfType<PlayerSkill>();
        cooltimeImage = transform.Find("Cooltime").GetComponent<Image>();
    }

    private void Update()
    {
        if (shortcut.skill == null) return;
        if (playerSkill.isSkillCool.ContainsKey(shortcut.skill) == false) return;
        if (playerSkill.isSkillCool[shortcut.skill])
            cooltimeImage.fillAmount = 1 - playerSkill.skillCool[shortcut.skill].GetTime / shortcut.skill.cooltime;
        else cooltimeImage.fillAmount = 0;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooltimeViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerSkill playerSkill;
    [SerializeField]
    private Image[] cooltimeImages;
    [SerializeField]
    private TextMeshProUGUI[] skillText;

    private void Update()
    {
        for (int i = 0; i < cooltimeImages.Length; i++)
        {
            if (playerSkill.isSkillCool[i])
                cooltimeImages[i].fillAmount = 1 - playerSkill.skillCoolTimer[i].GetTime / playerSkill.skills[i].cooltime;
            else cooltimeImages[i].fillAmount = 0;

            if (playerSkill.skills[i].skill != "")
                skillText[i].text = playerSkill.skills[i].skill;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooltimeViewer : MonoBehaviour
{
    private PlayerSkill playerSkill;
    private Image cooltimeImages;
    [SerializeField]
    private int index;

    private void Awake()
    {
        playerSkill = FindObjectOfType<PlayerSkill>();
        cooltimeImages = GetComponent<Image>();
    }

    private void Update()
    {
        if (playerSkill.isSkillCool[index])
            cooltimeImages.fillAmount = 1 - playerSkill.skillCoolTimer[index].GetTime / playerSkill.skills[index].cooltime;
        else cooltimeImages.fillAmount = 0;
    }
}

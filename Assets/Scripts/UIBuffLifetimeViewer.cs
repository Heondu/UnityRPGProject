using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBuffLifetimeViewer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Image cooltimeImage;
    private SkillBuff buff;
    private Skill skill;

    private void Awake()
    {
        cooltimeImage = transform.Find("Cooltime").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void Init(SkillBuff buff, Skill skill)
    {
        this.buff = buff;
        this.skill = skill;
        text.text = skill.name;
    }

    private void Update()
    {
        cooltimeImage.fillAmount = buff.currentTime / skill.lifetime;
    }
}

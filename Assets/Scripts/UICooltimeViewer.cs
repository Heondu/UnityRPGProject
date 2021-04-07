using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooltimeViewer : MonoBehaviour
{
    [SerializeField]
    private Shortcut shortcut;
    private Image cooltimeImage;

    private void Awake()
    {
        cooltimeImage = transform.Find("Cooltime").GetComponent<Image>();
    }

    private void Update()
    {
        if (shortcut.skill == null) return;
        if (shortcut.skill.isCool)
            cooltimeImage.fillAmount = 1 - shortcut.skill.currentTime / shortcut.skill.cooltime;
        else cooltimeImage.fillAmount = 0;
    }
}

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
        if (shortcut.skill == "") return;
        if (DataManager.skillDB[shortcut.skill].isCool)
            cooltimeImage.fillAmount = 1 - DataManager.skillDB[shortcut.skill].currentTime / DataManager.skillDB[shortcut.skill].cooltime;
        else cooltimeImage.fillAmount = 0;
    }
}

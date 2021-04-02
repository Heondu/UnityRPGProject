using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private GameObject inventory;
    private Toggle toggle;
    private Image buttonImage;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Color changedButtonColor;
    [SerializeField]
    private Color changedIconColor;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        buttonImage = GetComponent<Image>();
        toggle.onValueChanged.AddListener(OnToggle);
    }

    public void OnToggle(bool isOn)
    {
        if (isOn)
        {
            buttonImage.color = Color.white;
            iconImage.color = Color.white;
        }
        else
        {
            buttonImage.color = changedButtonColor;
            iconImage.color = changedIconColor;
        }
    }

    public void SetInventory(bool isOn)
    {
        inventory.SetActive(isOn);
        if (isOn) scrollRect.content = inventory.GetComponent<RectTransform>();
    }
}

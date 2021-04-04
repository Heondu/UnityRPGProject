using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    [SerializeField]
    private GameObject popup;
    private RectTransform popupRect;
    private Image icon;
    private Text name;
    private Text qualityName;
    private Text baseStatusTitle;
    private Text baseStatus;
    private Text addStatusTitle1;
    private Text addStatusTitle2;
    private Text addStatusTitle3;
    private Text addStatus1;
    private Text addStatus2;
    private Text addStatus3;

    private void Awake()
    {
        popupRect = popup.GetComponent<RectTransform>();
        icon = popup.transform.Find("ItemIcon").GetComponent<Image>();
        name = popup.transform.Find("Name").GetComponent<Text>();
        qualityName = popup.transform.Find("QualityName").GetComponent<Text>();
        baseStatusTitle = popup.transform.Find("BaseStatusTitle").GetComponent<Text>();
        baseStatus = popup.transform.Find("BaseStatusTitle").Find("Status").GetComponent<Text>();
        addStatusTitle1 = popup.transform.Find("StatusTitle (1)").GetComponent<Text>();
        addStatusTitle2 = popup.transform.Find("StatusTitle (2)").GetComponent<Text>();
        addStatusTitle3 = popup.transform.Find("StatusTitle (3)").GetComponent<Text>();
        addStatus1 = popup.transform.Find("StatusTitle (1)").Find("Status").GetComponent<Text>();
        addStatus2 = popup.transform.Find("StatusTitle (2)").Find("Status").GetComponent<Text>();
        addStatus3 = popup.transform.Find("StatusTitle (3)").Find("Status").GetComponent<Text>();
    }

    private void Update()
    {
        if (popup.activeSelf) PopupFollowMouse();
    }

    public void UpdatePopup(Slot selectedItem)
    {
        popup.SetActive(true);
        icon.sprite = selectedItem.itemIcon.sprite;
        name.text = $"{DataManager.Localization(selectedItem.item.nameAdd[0])} {DataManager.Localization(selectedItem.item.name)}";
        qualityName.text = $"{DataManager.Localization(selectedItem.item.rarityType)} {DataManager.Localization(selectedItem.item.type)}";
        baseStatusTitle.text = DataManager.Localization(selectedItem.item.status);
        baseStatus.text = DataManager.Localization(selectedItem.item.stat.ToString());
        addStatusTitle1.text = DataManager.Localization(selectedItem.item.statusAdd[0]);
        addStatus1.text = DataManager.Localization(selectedItem.item.statAdd[0].ToString());
        addStatusTitle2.text = DataManager.Localization(selectedItem.item.statusAdd[1]);
        addStatus2.text = DataManager.Localization(selectedItem.item.statAdd[1].ToString());
        addStatusTitle3.text = DataManager.Localization(selectedItem.item.statusAdd[2]);
        addStatus3.text = DataManager.Localization(selectedItem.item.statAdd[2].ToString());
    }

    public void ClosePopup()
    {
        popup.SetActive(false);
    }

    private void PopupFollowMouse()
    {
        popup.transform.position = Input.mousePosition;
        if (popup.transform.position.y < Screen.height / 2) popupRect.pivot = Vector2.zero;
        else popupRect.pivot = Vector2.up;
        if (popup.transform.position.x + popupRect.sizeDelta.x > Screen.width) popupRect.pivot = new Vector2(1, popupRect.pivot.y);
    }
}

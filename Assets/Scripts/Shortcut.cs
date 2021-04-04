using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shortcut : MonoBehaviour
{
    [SerializeField]
    private Transform shortcut;
    private Image icon;
    public Image cooltime;
    private TextMeshProUGUI keycodeSelf;
    private TextMeshProUGUI keycodeOther;
    private TextMeshProUGUI quantity;
    public string skill;
    [SerializeField]
    private KeyAction keycode;

    private void Awake()
    {
        icon = shortcut.Find("ItemIcon").GetComponent<Image>();
        cooltime = shortcut.Find("Cooltime").GetComponent<Image>();
        keycodeSelf = transform.Find("Keycode").GetComponent<TextMeshProUGUI>();
        keycodeOther = shortcut.Find("Keycode").GetComponent<TextMeshProUGUI>();
        if (shortcut.Find("Quantity") == true) quantity = shortcut.Find("Quantity").GetComponent<TextMeshProUGUI>();
        keycodeSelf.text = KeySetting.keys[keycode].ToString();
        keycodeOther.text = KeySetting.keys[keycode].ToString();
    }

    public void ShortcutAssign(Slot slot)
    {
        icon.sprite = slot.itemIcon.sprite;
        icon.color = slot.itemIcon.color;
        if (quantity != null) quantity.text = slot.quantityText.text;
        skill = slot.skill;
    }
}

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
    public Skill skill;
    [SerializeField]
    private KeyAction keycode;
    private Slot slot;
    [SerializeField]
    private string skillName;

    private void Awake()
    {
        icon = shortcut.Find("ItemIcon").GetComponent<Image>();
        cooltime = shortcut.Find("Cooltime").GetComponent<Image>();
        keycodeSelf = transform.Find("Keycode").GetComponent<TextMeshProUGUI>();
        keycodeOther = shortcut.Find("Keycode").GetComponent<TextMeshProUGUI>();
        if (shortcut.Find("Quantity") == true) quantity = shortcut.Find("Quantity").GetComponent<TextMeshProUGUI>();
        keycodeSelf.text = KeySetting.keys[keycode].ToString();
        keycodeOther.text = KeySetting.keys[keycode].ToString();
        slot = GetComponent<Slot>();
        if (skillName != "") skill = DataManager.skillDB[skillName];
    }

    private void Update()
    {
        ShortcutAssign();
    }

    private void ShortcutAssign()
    {
        if (slot == null) return;
        icon.sprite = slot.icon.sprite;
        icon.color = slot.icon.color;
        if (quantity != null) quantity.text = slot.quantityText.text;
        if (slot.item != null && slot.item.skill != null) skill = slot.item.skill;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject uIPanel;
    [SerializeField]
    private GameObject equipPanel;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject info;
    [SerializeField]
    private Toggle equipToggle;
    [SerializeField]
    private Toggle infoToggle;

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.inventory]))
        {
            if (uIPanel.activeSelf == false) uIPanel.SetActive(true);
            else if (equipToggle.isOn) uIPanel.SetActive(false);
            equipToggle.isOn = uIPanel.activeSelf;

        }
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.info]))
        {
            if (uIPanel.activeSelf == false) uIPanel.SetActive(true);
            else if (infoToggle.isOn) uIPanel.SetActive(false);
            infoToggle.isOn = uIPanel.activeSelf;
        }
    }
}

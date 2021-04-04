using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHpManaViewer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private string valueName1;
    [SerializeField]
    private string valueName2;
    private Status status;

    private void Awake()
    {
        status = FindObjectOfType<Player>().GetComponent<Status>();
    }

    private void Update()
    {
        image.fillAmount = status.status[valueName1] / status.status[valueName2];
        text.text = $"{(int)status.status[valueName1]}/{(int)status.status[valueName2]}";
    }
}

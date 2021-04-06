using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIExpViewer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TextMeshProUGUI textExp;
    private Status status;

    private void Awake()
    {
        //status = FindObjectOfType<Player>().GetComponent<Status>();
    }

    private void Update()
    {
        //image.fillAmount = status.status["exp"] / (int)DataManager.experience[(int)status.status["level"]]["exp"];
        //textLevel.text = status.status["level"].ToString();
        //textExp.text = $"{(status.status["exp"] / (int)DataManager.experience[(int)status.status["level"]]["exp"] * 100).ToString("N2")}%";
    }
}

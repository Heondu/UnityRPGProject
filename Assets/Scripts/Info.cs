using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField]
    private GameObject info;
    private Status playerStatus;
    private Dictionary<string, Text> statusTexts = new Dictionary<string, Text>();

    private void Awake()
    {
        playerStatus = FindObjectOfType<Player>().GetComponent<Status>();
        for (int i = 0; i < info.transform.Find("StatusText").childCount; i++)
        {
            statusTexts.Add(info.transform.Find("StatusText").GetChild(i).name, info.transform.Find("StatusText").GetChild(i).GetChild(0).GetComponent<Text>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.info]))
        {
            UpdateInfo();
            info.SetActive(!info.activeSelf);
        }
    }

    private void UpdateInfo()
    {
        statusTexts["strength"].text = playerStatus.status["strength"].ToString();
        statusTexts["agility"].text = playerStatus.status["agility"].ToString();
        statusTexts["intelligence"].text = playerStatus.status["intelligence"].ToString();
        statusTexts["damage"].text = playerStatus.status["damage"].ToString();
        statusTexts["fixDam"].text = playerStatus.status["fixDam"].ToString();
        statusTexts["critChance"].text = playerStatus.status["critChance"].ToString();
        statusTexts["avoidance"].text = playerStatus.status["avoidance"].ToString();
        statusTexts["accuracy"].text = playerStatus.status["accuracy"].ToString();
        statusTexts["reduceMana"].text = playerStatus.status["reduceMana"].ToString();
        statusTexts["reduceCooltime"].text = playerStatus.status["reduceCooltime"].ToString();
        statusTexts["defence"].text = playerStatus.status["defence"].ToString();
        statusTexts["critAvoid"].text = playerStatus.status["critAvoid"].ToString();
        statusTexts["critDamage"].text = playerStatus.status["critDamage"].ToString();
        statusTexts["fireResist"].text = playerStatus.status["fireResist"].ToString();
        statusTexts["coldResist"].text = playerStatus.status["coldResist"].ToString();
        statusTexts["darkResist"].text = playerStatus.status["darkResist"].ToString();
        statusTexts["lightResist"].text = playerStatus.status["lightResist"].ToString();
        statusTexts["fireDamage"].text = playerStatus.status["fireDamage"].ToString();
        statusTexts["coldDamage"].text = playerStatus.status["coldDamage"].ToString();
        statusTexts["darkDamage"].text = playerStatus.status["darkDamage"].ToString();
        statusTexts["lightDamage"].text = playerStatus.status["lightDamage"].ToString();
    }
}

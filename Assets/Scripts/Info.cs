using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField]
    private GameObject info;
    private Status playerStatus;
    private Dictionary<string, Text> statusTexts = new Dictionary<string, Text>();
    private Dictionary<string, Text> status = new Dictionary<string, Text>();

    private void Awake()
    {
        playerStatus = FindObjectOfType<Player>().GetComponent<Status>();
        for (int i = 0; i < info.transform.Find("StatusText").childCount; i++)
        {
            statusTexts.Add(info.transform.Find("StatusText").GetChild(i).name, info.transform.Find("StatusText").GetChild(i).GetComponent<Text>());
            status.Add(info.transform.Find("StatusText").GetChild(i).name, info.transform.Find("StatusText").GetChild(i).GetChild(0).GetComponent<Text>());
        }

        foreach (string key in statusTexts.Keys)
            if (playerStatus.status.ContainsKey(key))
                statusTexts[key].text = DataManager.Localization(key);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.info])) UpdateInfo();
    }

    private void UpdateInfo()
    {
        foreach (string key in status.Keys)
            if (playerStatus.status.ContainsKey(key))
                status[key].text = playerStatus.status[key].ToString();
    }
}

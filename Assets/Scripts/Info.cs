﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    [SerializeField]
    private GameObject info;
    private Player player;
    [SerializeField]
    private Transform[] statusArray;
    private Dictionary<string, TextMeshProUGUI> statName = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, TextMeshProUGUI> statValue = new Dictionary<string, TextMeshProUGUI>();

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        for (int i = 0; i < statusArray.Length; i++)
        {
            statName.Add(statusArray[i].name, statusArray[i].Find("statName").GetComponent<TextMeshProUGUI>());
            statValue.Add(statusArray[i].name, statusArray[i].Find("statValue").GetComponent<TextMeshProUGUI>());
        }

        foreach (string key in statName.Keys)
        {
            Status playerStatus = player.status.GetStatus(key);
            if (playerStatus != null)
                statName[key].text = DataManager.Localization(key);
        }
    }

    private void OnEnable()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        foreach (string key in statValue.Keys)
        {
            Status playerStatus = player.status.GetStatus(key);
            if (playerStatus != null)
                statValue[key].text = playerStatus.Value.ToString("0");
        }
    }
}

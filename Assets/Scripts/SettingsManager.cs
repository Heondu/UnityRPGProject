﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingsManager : MonoBehaviour
{
    static SettingsManager _settingsManager;

    private float BGM_volume;
    private float SE_volume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static SettingsManager settingsManager
    {
        get
        {
            if (!_settingsManager)
            {
                GameObject go;
                if (GameObject.Find("Managers") == null)
                {
                    go = new GameObject("Managers");
                }
                else
                {
                    go = GameObject.Find("Managers");
                }
                _settingsManager = go.AddComponent<SettingsManager>();
                _settingsManager.Resetting();
            }
            return _settingsManager;
        }
    }

    public void Resetting()
    {
        if (PlayerPrefs.HasKey("BGM"))
        {
            BGM_volume = PlayerPrefs.GetFloat("BGM");//PlayerPref에 옵션 정보가 있으면 로드하고 없으면 0.7로 설정
        }
        else
        {
            BGM_volume = 0.7f;
        }

        if (PlayerPrefs.HasKey("SE"))
        {
            SE_volume = PlayerPrefs.GetFloat("SE");
        }
        else
        {
            SE_volume = 0.7f;
        }
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("BGM", settingsManager.BGM_volume);
        PlayerPrefs.SetFloat("SE", settingsManager.SE_volume);
    }


    public static void Reset_now()
    {
        settingsManager.Resetting();
    }

    public static float getBGM
    {
        get
        {
            return settingsManager.BGM_volume;
        }
    }
    public static void setBGM(float value)
    {
        settingsManager.BGM_volume = value;
    }
    public static float getSE
    {
        get
        {
            return settingsManager.SE_volume;
        }
    }
    public static void setSE(float value)
    {
        settingsManager.SE_volume = value;
    }
}

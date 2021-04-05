using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private Status status;
    [SerializeField]
    private StatusData statusData;
    private int statusPoint = 0;
    public Dictionary<string, float> baseStatus = new Dictionary<string, float>();

    private void Awake()
    {
        status = GetComponent<Status>();
        statusData = new StatusData();
        //JsonIO.SaveStatusToJson(status.status, "PlayerStatus");
        status.status = JsonIO.LoadStatusFromJson("PlayerStatus");
        baseStatus["strength"] = status.status["strength"];
        baseStatus["agility"] = status.status["agility"];
        baseStatus["intelligence"] = status.status["intelligence"];
        baseStatus["endurance"] = status.status["endurance"];
    }

    private void Update()
    {
        if (status.status["exp"] >= (int)DataManager.experience[(int)status.status["level"]]["exp"])
        {
            status.status["exp"] -= (int)DataManager.experience[(int)status.status["level"]]["exp"];
            status.status["level"] += 1f;
            statusPoint += 10;
        }

        statusData.DicToVar(status.status);
    }

    private void PrintStatus()
    {
        foreach (string key in status.status.Keys)
            Debug.Log(status.status[key]);
    }
}

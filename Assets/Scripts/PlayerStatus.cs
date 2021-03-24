using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private Status status;
    private int statusPoint;
    public Dictionary<string, object> fourStatus = new Dictionary<string, object>();

    private void Awake()
    {
        status = GetComponent<Status>();
        status.status = JsonIO.LoadFromJson<StringFloat>("PlayerStatus");
        fourStatus["strength"] = (int)status.status["strength"];
        fourStatus["agility"] = (int)status.status["agility"];
        fourStatus["intelligence"] = (int)status.status["intelligence"];
        fourStatus["endurance"] = (int)status.status["endurance"];
    }
}

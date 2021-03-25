using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenManager : MonoBehaviour
{
    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        GameObject[] regens = GameObject.FindGameObjectsWithTag("RegenArea");
        foreach (GameObject regen in regens)
        {
            RegenArea regenArea = regen.GetComponent<RegenArea>();
            regen.AddComponent<RegenMonster>().SpawnMonster(regenArea.monType, regenArea.regenNumMax, regenArea.size);
        }
    }
}

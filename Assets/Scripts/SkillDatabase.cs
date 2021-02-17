using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public List<Skill> skillDB = new List<Skill>();
}

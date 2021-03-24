using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SkillBuff : MonoBehaviour
{
    public delegate void Callback(Skill skill);
    private Callback callback = null;
    private Skill skill;
    private Timer timer = new Timer();

    public void Execute(GameObject executor, Skill skill)
    {
        this.skill = skill;
        StatusCalculator.SkillStatusCalc(executor.GetComponent<Status>().status, null, skill);
        StartCoroutine("TimeOut");
    }

    private IEnumerator TimeOut()
    {
        while (true)
        {
            if (timer.IsTimeOut((float)skill.status["lifetime"]))
            {
                callback(skill);
                Destroy(gameObject);
            }

            yield return null;
        }
    }

    public void SetCallBack(Callback callback)
    {
        this.callback = callback;
    }
}

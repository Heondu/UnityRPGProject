using UnityEngine;

public class SkillBuff : SkillScript
{
    protected override void Update()
    {
        base.Update();
    }

    public override void Execute(GameObject executor, Skill skill)
    {
        base.Execute(executor, skill);
        StatusCalculator.SkillStatusCalc(executor.GetComponent<Status>().status, null, skill);
    }
}

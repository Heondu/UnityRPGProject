using UnityEngine;

public class SkillScript : MonoBehaviour
{
    public delegate void Callback(Skill skill);
    protected Callback callback = null;
    protected Timer timer = new Timer();
    protected GameObject executor;
    protected Skill skill;

    protected virtual void Update()
    {
        if (skill != null)
        {
            if (timer.IsTimeOut(float.Parse(skill.status["lifetime"].ToString()))) Destroy(gameObject);
        }
    }

    public virtual void Execute(GameObject executor, Skill skill)
    {
        this.executor = executor;
        this.skill = skill;
    }

    public void SetCallBack(Callback callback)
    {
        this.callback = callback;
    }
}

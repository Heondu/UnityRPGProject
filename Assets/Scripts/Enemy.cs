using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class Enemy : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private EnemyController enemyController;
    private Health health;
    private EnemyState state = EnemyState.STATE_PATROL;
    private string name;
    public Status status;
    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();

    private void Awake()
    {
        movement = GetComponent<Movement>();
        enemyController = GetComponent<EnemyController>();
        health = GetComponent<Health>();
        status = GetComponent<Status>();
    }

    private void Update()
    {
        ChangeState(enemyController.Operate());

        switch (state)
        {
            case EnemyState.STATE_PATROL:
                movement.Execute(enemyController.GetAxis());
                break;
            case EnemyState.STATE_CHASE:
                movement.Execute(enemyController.GetAxis());
                break;
            case EnemyState.STATE_ATTACK:
                //attack.Execute((int)status.status["damage"]);
                break;
        }
    }

    private void ChangeState(EnemyState state)
    {
        this.state = state; 
    }

    public void Init(string name)
    {
        this.name = name;
        monster = DataManager.monster.FindDic("name", name);
        monlvl = DataManager.monlvl.FindDic("Level", monster["monlvl"]);
        StatusCalculator.StatusCalc(status.status, monlvl);
    }

    public void TakeDamage()
    {
        if (status.status["HP"] <=  0)
        {
            ItemGenerator.Instance.DropItem((int)monlvl["raritymin"], (int)monlvl["raritymax"], monster["class"].ToString(), transform.position);
            Destroy(gameObject);
        }
    }
}

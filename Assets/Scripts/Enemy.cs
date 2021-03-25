using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class Enemy : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private Attack attack;
    private EnemyController enemyController;
    private Health health;
    private EnemyState state = EnemyState.STATE_PATROL;
    [SerializeField]
    public string name;
    public Status status;
    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponentInChildren<Attack>();
        enemyController = GetComponent<EnemyController>();
        health = GetComponent<Health>();
        status = GetComponent<Status>();
        Init();
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
                attack.Execute((int)status.status["damage"]);
                break;
        }
    }

    private void ChangeState(EnemyState state)
    {
        this.state = state; 
    }

    private void Init()
    {
        monster = DataManager.Find(DataManager.monster, "name", name);
        monlvl = DataManager.Find(DataManager.monlvl, "Level", monster["monlvl"]);
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

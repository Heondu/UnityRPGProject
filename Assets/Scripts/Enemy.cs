using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class Enemy : MonoBehaviour
{
    private Movement movement;
    private Attack attack;
    private EnemyController enemyController;
    private EnemyState state = EnemyState.STATE_PATROL;
    [SerializeField]
    private string name;
    [SerializeField]
    private Status status;
    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponent<Attack>();
        enemyController = GetComponent<EnemyController>();

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
                attack.Execute(status.damage);
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
        status.strength = (int)monlvl["strength"];
        status.agility = (int)monlvl["agility"];
        status.intelligence = (int)monlvl["intelligence"];
        status.endurance = (int)monlvl["endurance"];
        status.Init();
    }
}

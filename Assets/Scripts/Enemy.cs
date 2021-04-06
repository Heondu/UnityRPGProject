using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class Enemy : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private EnemyController enemyController;
    private EnemyAttack enemyAttack;
    private EnemyState state = EnemyState.STATE_PATROL;
    [SerializeField]
    private GameObject damagePrefab;
    private new string name;
    public EntityStatus status;
    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();

    private void Awake()
    {
        movement = GetComponent<Movement>();
        enemyController = GetComponent<EnemyController>();
        enemyAttack = GetComponent<EnemyAttack>();
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
                enemyAttack.Execute(1);
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
        status.maxHP = 10;
        status.HP = status.maxHP;
        status.strength.BaseValue = (int)monlvl["strength"];
        status.agility.BaseValue = (int)monlvl["agility"];
        status.intelligence.BaseValue = (int)monlvl["intelligence"];
        status.endurance.BaseValue = (int)monlvl["endurance"];
        status.CalculateDerivedStatus();
    }

    public void TakeDamage(int damage)
    {
        FloatingDamage(damage);
        status.HP = Mathf.Max(0, status.HP - damage);
        if (status.HP ==  0)
        {
            FindObjectOfType<Player>().status.exp += (int)monlvl["monexp"];
            ItemGenerator.Instance.DropItem((int)monlvl["raritymin"], (int)monlvl["raritymax"], monster["class"].ToString(), transform.position);
            Destroy(gameObject);
        }
    }

    public void FloatingDamage(int damage)
    {
        GameObject clone = Instantiate(damagePrefab, GameObject.Find("Canvas").transform);
        clone.GetComponent<FloatingDamage>().Init(damage, transform.position);
    }
}

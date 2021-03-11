using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class Enemy : MonoBehaviour
{
    private Movement movement;
    private Attack attack;
    private EnemyController enemyController;
    private EnemyState state = EnemyState.STATE_PATROL;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponent<Attack>();
        enemyController = GetComponent<EnemyController>();
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
                attack.Execute();
                break;
        }
    }

    private void ChangeState(EnemyState state)
    {
        this.state = state; 
    }
}

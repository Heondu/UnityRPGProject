using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private PlayerInput playerInput;
    private PlayerItem playerItem;
    public PlayerSkill playerSkill;
    private PlayerAnimator playerAnimator;
    public PlayerStatus playerStatus;
    public Status status;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerInput = GetComponent<PlayerInput>();
        playerItem = GetComponent<PlayerItem>();
        playerSkill = GetComponent<PlayerSkill>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerStatus = GetComponent<PlayerStatus>();
        status = GetComponent<Status>();
        StatusCalculator.StatusCalc(status.status, playerStatus.fourStatus);
    }

    private void Update()
    {
        playerAnimator.Movement(playerInput.GetAxis());
        if (IsMove()) movement.Execute(playerInput.GetAxis());
        if (IsAttack()) playerSkill.Execute(playerInput.GetSkillIndex(), gameObject);
    }

    private bool IsMove()
    {
        return true;
    }

    private bool IsAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerInput.GetSkillIndex() == i)
            {
                if (playerSkill.IsSkillCool[i] == false) return true;
            }
        }
        return false;
    }

    public void TakeDamage()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemScript>() != null)
            collision.GetComponent<ItemScript>().Use(playerItem);
    }
}

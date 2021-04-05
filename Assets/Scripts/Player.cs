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
        StatusCalculator.StatusCalc(status.status, playerStatus.baseStatus);
    }

    private void Update()
    {
        playerAnimator.Movement(playerInput.GetAxis());
        if (!IsMove()) movement.Execute(playerInput.GetAxis());
        if (!IsAttack()) playerSkill.Execute(playerInput.GetSkillIndex(), gameObject);
        if (!IsItemCool()) playerItem.Execute(playerInput.GetItemIndex(), gameObject);
    }

    private bool IsMove()
    {
        return false;
    }

    private bool IsAttack()
    {
        for (int i = 0; i < 4; i++)
            if (playerInput.GetSkillIndex() == i) return false;
        return true;
    }

    private bool IsItemCool()
    {
        for (int i = 0; i < 4; i++)
            if (playerInput.GetItemIndex() == i) return false;
        return true;
    }

    public void TakeDamage(int damage)
    {
        status.status["HP"] = Mathf.Max(0, status.status["HP"] - damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemScript>() != null)
            collision.GetComponent<ItemScript>().Use(playerItem);
    }
}

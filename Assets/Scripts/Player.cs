using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private Attack attack;
    private PlayerInput playerInput;
    private PlayerItem playerItem;
    private PlayerSkill playerSkill;
    private PlayerAnimator playerAnimator;
    private PlayerStatus playerStatus;
    private Rotation rotation;
    public Status status;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponentInChildren<Attack>();
        playerInput = GetComponent<PlayerInput>();
        playerItem = GetComponent<PlayerItem>();
        playerSkill = GetComponent<PlayerSkill>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerStatus = GetComponent<PlayerStatus>();
        rotation = GetComponentInChildren<Rotation>();
        status = GetComponent<Status>();
        StatusCalculator.StatusCalc(status.status, playerStatus.fourStatus);
    }

    private void Update()
    {
        playerAnimator.Movement(playerInput.GetAxis());
        if (playerInput.IsMove()) movement.Execute(playerInput.GetAxis());
        if (playerInput.IsAttack()) playerSkill.Execute(playerInput.GetSkillIndex(), gameObject);
        rotation.Rotate(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
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

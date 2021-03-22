using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private Attack attack;
    private PlayerInput playerInput;
    private PlayerItem playerItem;
    private PlayerAnimator playerAnimator;
    private Rotation rotation;
    public Status status;
    private int HP = 0;
    private int maxHP = 100;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponentInChildren<Attack>();
        playerInput = GetComponent<PlayerInput>();
        playerItem = GetComponent<PlayerItem>();
        playerAnimator = GetComponent<PlayerAnimator>();
        rotation = GetComponentInChildren<Rotation>();
        status = GetComponent<Status>();
        HP = maxHP;
        status.StatusCalc();
    }

    private void Update()
    {
        playerAnimator.Movement(playerInput.GetAxis());
        if (playerInput.IsMove()) movement.Execute(playerInput.GetAxis());
        if (playerInput.IsAttack()) attack.Execute((int)status.status["damage"]);
        rotation.Rotate(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    public void TakeDamage(int damage)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemScript>() != null)
            collision.GetComponent<ItemScript>().Use(playerItem);
    }
}

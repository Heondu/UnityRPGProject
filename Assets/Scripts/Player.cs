using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private Attack attack;
    private PlayerInput playerInput;
    private PlayerItem playerItem;
    [SerializeField]
    public Status status;
    private int HP = 0;
    private int maxHP = 100;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponent<Attack>();
        playerInput = GetComponent<PlayerInput>();
        playerItem = GetComponent<PlayerItem>();
        HP = maxHP;
        status.StatusCalc();
    }

    private void Update()
    {
        if (playerInput.IsMove()) movement.Execute(playerInput.GetAxis());
        if (playerInput.IsAttack()) attack.Execute(status.damage);
    }

    [ContextMenu("StatusCalc")]
    public void StatusCalc()
    {
        playerItem.StatusCalc();
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

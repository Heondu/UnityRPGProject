using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    private Attack attack;
    private PlayerInput playerInput;
    private PlayerItem playerItem;
    [SerializeField]
    private Status status;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponent<Attack>();
        playerInput = GetComponent<PlayerInput>();
        playerItem = GetComponent<PlayerItem>();

        status.Init();
    }

    private void Update()
    {
        if (playerInput.IsMove()) movement.Execute(playerInput.GetAxis());
        if (playerInput.IsAttack()) attack.Execute(status.damage);
    }

    [ContextMenu("Init")]
    private void Init()
    {
        status.Init();
    }
}

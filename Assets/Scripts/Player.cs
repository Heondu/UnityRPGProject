using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    private Attack attack;
    private PlayerInput playerInput;
    [SerializeField]
    private Status status = new Status();

    private void Awake()
    {
        movement = GetComponent<Movement>();
        attack = GetComponent<Attack>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.IsMove()) movement.Execute(playerInput.GetAxis());
        if (playerInput.IsAttack()) attack.Execute();
    }
}

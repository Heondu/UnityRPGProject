using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    private Attack attack;
    private PlayerInput playerInput;
    private Status status;

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

using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private PlayerInput playerInput;
    private PlayerAnimator playerAnimator;
    public PlayerStatus status;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<PlayerAnimator>();
        LoadStatus();
    }

    private void Update()
    {
        playerAnimator.Movement(playerInput.GetAxis());
        if (!IsMove()) movement.Execute(playerInput.GetAxis());

        status.CalculateDerivedStatus();
    }

    private bool IsMove()
    {
        return false;
    }

    public void TakeDamage(float _value, DamageType damageType)
    {
        int value = Mathf.RoundToInt(_value);

        if (damageType == DamageType.miss) FloatingDamageManager.instance.FloatingDamage("Miss", transform.position, damageType);
        else FloatingDamageManager.instance.FloatingDamage(value.ToString(), transform.position, damageType);

        if (damageType == DamageType.normal)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageType == DamageType.critical)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageType == DamageType.heal)
        {
            status.HP = Mathf.Min(status.HP + value, status.maxHP);
        }
    }

        public Status GetStatus(string name)
    {
        return status.GetStatus(name);
    }

    [ContextMenu("Save Status")]
    public void SaveStatus()
    {
        JsonIO.SaveToJson(status, "PlayerStatus");
    }

    [ContextMenu("Load Status")]
    public void LoadStatus()
    {
        status = JsonIO.LoadFromJson<PlayerStatus>("PlayerStatus");
    }
}

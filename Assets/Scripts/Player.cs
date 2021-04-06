using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private PlayerInput playerInput;
    private PlayerItem playerItem;
    public PlayerSkill playerSkill;
    private PlayerAnimator playerAnimator;
    public EntityStatus status;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerInput = GetComponent<PlayerInput>();
        playerItem = GetComponent<PlayerItem>();
        playerSkill = GetComponent<PlayerSkill>();
        playerAnimator = GetComponent<PlayerAnimator>();
        LoadStatus();
        
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
        status.HP = Mathf.Max(0, status.HP - damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemScript>() != null)
            collision.GetComponent<ItemScript>().Use(playerItem);
    }

    [ContextMenu("Save Status")]
    public void SaveStatus()
    {
        JsonIO.SaveToJson(status, "PlayerStatus");
    }

    [ContextMenu("Load Status")]
    public void LoadStatus()
    {
        status = JsonIO.LoadFromJson<EntityStatus>("PlayerStatus");
    }

    [ContextMenu("Calculate Derived Status")]
    public void CalculateDerivedStatus()
    {
        status.CalculateDerivedStatus();
        Debug.Log(status.damage.Value);
        Debug.Log(status.fixDamage.Value);
    }
}

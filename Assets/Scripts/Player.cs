using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    public Status status = new Status(0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    public Item item = new Item("한손검", 5, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    public Skill skill = new Skill("휘두르기", Type.fire, 0, 3, false, 0, false, 0, 0, 0, 0, 0);

    private void Start()
    {
        Status.StatusCalc(status, item.status, true);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }
}

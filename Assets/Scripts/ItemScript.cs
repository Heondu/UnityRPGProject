using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Item item;
    public string skill;

    public void Use(PlayerItem playerItem)
    {
        item.skill = skill;
        playerItem.PickUp(item);
        Destroy(gameObject);
    }
}

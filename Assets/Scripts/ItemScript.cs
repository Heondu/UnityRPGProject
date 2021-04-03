using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Item item;

    public void Use(PlayerItem playerItem)
    {
        playerItem.PickUp(item);
        Destroy(gameObject);
    }
}

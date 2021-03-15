using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField]
    public Item item;

    public void Use(PlayerItem playerItem)
    {
        playerItem.Equip(item);
        Destroy(gameObject);
    }
}

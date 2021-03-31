using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item item = null;
    public Image itemIcon;
    public Image qualty;
    public Text quantity;

    private void Awake()
    {
        itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        qualty = transform.Find("Quality").GetComponent<Image>();
        quantity = transform.Find("Quantity").GetComponent<Text>();
    }
}

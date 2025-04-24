using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public Item Item;

    public InventoryItem(Item item, int quantity)
    {
        Item = item;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    private InventoryItem _itemData;

    public void Set(InventoryItem item)
    {
        _itemData = item;
        icon.sprite = item.Item.Icon;
    }

   
}

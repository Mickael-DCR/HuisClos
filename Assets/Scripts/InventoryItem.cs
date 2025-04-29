using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Item Item;
    [SerializeField] private Image _icon;

    public void InitialiseItem(Item item)
    {
        Item = item;
        _icon.sprite = item.Icon;
    }

    void Start()
    {
        InitialiseItem(Item);
    }
}

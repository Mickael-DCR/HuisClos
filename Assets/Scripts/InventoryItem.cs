using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Item _item;
    [SerializeField] private Image _icon;


    public void InitialiseItem(Item item)
    {
        _item = item;
        _icon.sprite = item.Icon;
    }

    void Start()
    {
        InitialiseItem(_item);
    }

    public void EquipItem()
    {
        var playerHand = InventoryManager.Instance.HandSlot;
        if (playerHand.childCount > 0)
        {
            Destroy(playerHand.GetChild(0).gameObject);
        }
        Instantiate(_item.ItemPrefab3D, playerHand);
    }

    public void InspectItem()
    {
        
    }
}

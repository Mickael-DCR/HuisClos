using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Item _item;
    [SerializeField] private Image _icon;
    private ItemInspector _inspector;


    public void InitialiseItem(Item item)
    {
        _item = item;
        _icon.sprite = item.Icon;
    }

    void Start()
    {
        InitialiseItem(_item);
        _inspector = ItemInspector.Instance;
    }

    public void EquipItem() // primary action 
    {
        var playerHand = InventoryManager.Instance.HandSlot;
        if (playerHand.childCount > 0)
        {
            Destroy(playerHand.GetChild(0).gameObject);
        }
        Instantiate(_item.ItemPrefab3D, playerHand);
    }

    public void InspectItem() //secondary action
    {
        _inspector.InspectWindow.SetActive(true);
        if (_inspector.InspectSlot.childCount > 0)
        {
            Destroy(_inspector.InspectSlot.GetChild(0).gameObject);
        }
        Instantiate(_item.ItemPrefab3D, _inspector.InspectSlot);
    }
}

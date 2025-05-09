using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item Item;
    [SerializeField] private Image _icon;
    private ItemInspector _inspector;


    public void InitialiseItem(Item item)
    {
        Item = item;
        if(_icon!=null) _icon.sprite = item.Icon;
    }

    void Start()
    {
        InitialiseItem(Item);
        _inspector = ItemInspector.Instance;
    }

    public void EquipItem() // primary action 
    {
        var playerHand = InventoryManager.Instance.HandSlot;
        if (playerHand.childCount > 0)
        {
            Destroy(playerHand.GetChild(0).gameObject);
        }
        Instantiate(Item.ItemPrefab3D, playerHand);
    }

    public void InspectItem() //secondary action
    {
        _inspector.InspectWindow.SetActive(true);
        _inspector.InventoryWindow.SetActive(false);
        _inspector.PlayerCamera.SetActive(false);
        if (_inspector.InspectSlot.childCount > 0)
        {
            Destroy(_inspector.InspectSlot.GetChild(0).gameObject);
        }
        Instantiate(Item.ItemPrefabInspect, _inspector.InspectSlot);
    }
}

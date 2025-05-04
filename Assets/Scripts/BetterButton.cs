using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BetterButton : MonoBehaviour, IPointerClickHandler
{
    private InventoryItem _inventoryItem;
    private void Awake()
    {
        _inventoryItem = GetComponentInParent<InventoryItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnLeftClick();
                break;

            case PointerEventData.InputButton.Right:
                OnRightClick();
                break;
        }
    }

    private void OnLeftClick()
    {
        _inventoryItem.EquipItem();
    }

    private void OnRightClick()
    {
        _inventoryItem.InspectItem();
    }
}
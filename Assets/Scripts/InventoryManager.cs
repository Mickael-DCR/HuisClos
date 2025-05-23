using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject InventoryWindow;
    [SerializeField] private InventorySlot[] inventorySlots;
    public Transform HandSlot;
    [SerializeField] private Item _candle;
    private Item _swapItem;
    private void Awake()
    {
        if(Instance == null)Instance = this;
    }
    
    private void Start()
    {
        if (PlayerController.InputSystemActions != null)
        {
            PlayerController.InputSystemActions.Player.EquipCandle.performed += EquipCandle;
        }
    }
    private void OnEnable()
    {
        PlayerController.InputSystemActions.Player.EquipCandle.performed += EquipCandle;
    }

    private void OnDisable()
    {
        PlayerController.InputSystemActions.Player.EquipCandle.Disable();
    }
    
    
    private void EquipCandle(InputAction.CallbackContext ctx)
    {
        if (HandSlot.childCount > 0)
        {
            var objectInHand = HandSlot.GetChild(0);
            if (!objectInHand.CompareTag("Candle"))
            {
                _swapItem = objectInHand.GetComponent<Collectible>().Item;
            }
            if (objectInHand.CompareTag("Candle"))
            {
                if (_swapItem != null)
                {
                    Destroy(objectInHand.gameObject);
                    Instantiate(_swapItem.ItemPrefab3D, HandSlot);
                }
            }
            else
            {
                Destroy(objectInHand.gameObject);
                Instantiate(FindItem(_candle).ItemPrefab3D, HandSlot);
            }
        }
        else
        {
            if(FindItem(_candle) != null) Instantiate(FindItem(_candle).ItemPrefab3D, HandSlot);
        }
    }
    public Item FindItem(Item itemToFind)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.Item == itemToFind)
            {
                return itemInSlot.Item;
            }
        }
        return null;
    }
    public bool AddItem(Item item, GameObject objectToDestroy)
    {
        //Looks for an empty slot
        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                Destroy(objectToDestroy);
                return true; // item added
            }
        }
        return false; // no space in inventory
    }

    
    
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGObject = Instantiate(item.ItemPrefabUI, slot.transform); 
        InventoryItem inventoryItem = newItemGObject.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void RemoveItem(Item itemToRemove)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.Item == itemToRemove)
            {
                if(_swapItem==itemInSlot.Item)_swapItem = null;
                ItemTooltipUI.Instance.HideTooltip(itemInSlot.Item); // if item is displayed in tooltip, display the exemple
                Destroy(itemInSlot.gameObject);
            }
        }
    }
    
}

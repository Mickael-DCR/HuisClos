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
            if (objectInHand.CompareTag("Candle"))return;
        }
        if(FindItem(_candle)) return;
        Debug.Log("No candle in inventory");
        
    }
    public bool FindItem(Item itemToFind)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.Item == itemToFind)
            {
                Instantiate(itemInSlot.Item.ItemPrefab3D, HandSlot);
                return true;
            }
        }
        return false;
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
    
    
}

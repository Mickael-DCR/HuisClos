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
    private void Awake()
    {
        if(Instance == null)Instance = this;
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

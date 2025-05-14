using System;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsReceptor : Prop
{
    [Header("Placement Settings")]
    [SerializeField] private Transform[] _parents;  // Multiple parents for multiple items
    [SerializeField] private GameObject[] _prefabs; // Multiple prefabs for multiple items
    private bool[] _areItemsPlaced;                 // prevents from placing the same item twice
    

    public bool RewardActive;

    private void Awake()
    {
        _areItemsPlaced = new bool[_solverTags.Length];
    }

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved && !RewardActive)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            if (playerHand.childCount == 0) return;

            Transform objectInHand = playerHand.GetChild(0);
            Item itemToRemove = objectInHand.GetComponent<Collectible>().Item;
            // Check if this item matches any required tags
            for (int i = 0; i < _solverTags.Length; i++)
            {
                if (!_areItemsPlaced[i] && objectInHand.CompareTag(_solverTags[i]))
                {
                    Instantiate(_prefabs[i], _parents[i], false) ;
                    _areItemsPlaced[i] = true;
                    InventoryManager.Instance.RemoveItem(itemToRemove); // Remove item from player's inventory
                    Destroy(objectInHand.gameObject); // Remove item from player's hand
                    Debug.Log($"Placed item: {_solverTags[i]}");
                    break;
                }
            }

            // Check if all required items are placed
            if (Array.TrueForAll(_areItemsPlaced, placed => placed))
            {
                RewardActive = true;
                Debug.Log("All required items placed. Reward activated.");
            }
        }
    }
    
    private void PickUp()
    {
        for (int i = 0; i < _solverTags.Length; i++)
        {
            var itemToAdd = _parents[i].GetChild(0).GetComponent<Collectible>().Item;
            if(itemToAdd != null)
            {
                InventoryManager.Instance.AddItem(itemToAdd, _parents[i].GetChild(0).gameObject);
                _areItemsPlaced[i] = false;
                RewardActive = false;
                break;
            }
        }
    }
}
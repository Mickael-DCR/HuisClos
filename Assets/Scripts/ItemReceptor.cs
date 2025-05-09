using UnityEngine;

public class ItemReceptor : Prop
{
    [Header("Placement Settings")]
    [SerializeField] private Transform[] _parents; // Multiple parents for multiple items
    private bool[] _itemsPlaced;

    public bool RewardActive;

    private void Awake()
    {
        _itemsPlaced = new bool[_solverTags.Length];
    }

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved && !RewardActive)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            if (playerHand.childCount == 0) return;

            Transform objectInHand = playerHand.GetChild(0);
            
            // Check if this item matches any required tags
            for (int i = 0; i < _solverTags.Length; i++)
            {
                if (!_itemsPlaced[i] && objectInHand.CompareTag(_solverTags[i]))
                {
                    Instantiate(objectInHand.gameObject, _parents[i].position, _parents[i].rotation);
                    _itemsPlaced[i] = true;
                    Destroy(objectInHand.gameObject); // Remove item from player's hand
                    Debug.Log($"Placed item: {_solverTags[i]}");
                    break;
                }
            }

            // Check if all required items are placed
            if (System.Array.TrueForAll(_itemsPlaced, placed => placed))
            {
                RewardActive = true;
                Debug.Log("All required items placed. Reward activated.");
            }
        }
    }
}
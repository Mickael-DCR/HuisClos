using UnityEngine;

public class ItemsReceptor : Prop
{
    [Header("Placement Settings")]
    [SerializeField] private Transform[] _parents;  // Multiple parents for multiple items
    [SerializeField] private GameObject[] _prefabs; // Multiple prefabs for multiple items
    private bool[] _itemsPlaced;                    // prevents from placing the same item twice
    

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
                    Instantiate(_prefabs[i], _parents[i], false) ;
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
                Destroy(GetComponent<Collider>());
                Debug.Log("All required items placed. Reward activated.");
            }
        }
    }
}
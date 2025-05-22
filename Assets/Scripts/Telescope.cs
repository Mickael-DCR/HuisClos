using UnityEngine;

public class Telescope : Prop
{
    [SerializeField] private GameObject _reward;
    [SerializeField] private Transform _spawnPoint;
    private bool _rewardActive;
    public override bool Interact()
    {
        UIManager.Instance.ToggleTelescope(true);
        return true;
    }

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved && !_rewardActive)
        {
            var objectInHand = InventoryManager.Instance.HandSlot.GetChild(0); // 3D object in player's hand
            var itemInHand = objectInHand.GetComponent<Collectible>().Item;             // 2D object in player's inventory
            InventoryManager.Instance.RemoveItem(itemInHand);
            Destroy(objectInHand.gameObject);
            UIManager.Instance.TelescopeMissingPiece.SetActive(true);
           // Instantiate(_reward,_spawnPoint);
            _rewardActive = true;
        }
    }
}

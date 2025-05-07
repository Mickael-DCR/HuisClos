using UnityEngine;
using UnityEngine.Serialization;

public class ItemReceptor : Prop
{
    [SerializeField] private Transform _parent;
    public bool RewardActive;
    
    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved && !RewardActive)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            Instantiate(playerHand.GetChild(0).gameObject, _parent);
            RewardActive = true;
        }
    }
}

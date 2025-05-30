using System;
using UnityEngine;

public class PuzzlePiece : Prop
{
    [SerializeField] private Pivot _parentPivot;
    [SerializeField] private GameObject _piecePrefab;
    

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved)
        {
                var hand = InventoryManager.Instance.HandSlot;
                var inventoryItem = hand.GetChild(0).GetComponent<Collectible>();
                var newProp = Instantiate( inventoryItem.Item.ItemPrefab3D, _parentPivot.transform.position, Quaternion.Euler(240,0,0));
                _parentPivot.Target = newProp;
                
                _parentPivot.EmissiveTarget = newProp.GetComponent<EmissiveChanger>();
                _parentPivot.FirstTimeSpawned = true;
                
                Destroy(hand.GetChild(0).gameObject);   // destroys object in hand
                InventoryManager.Instance.RemoveItem(inventoryItem.Item);
                Destroy(gameObject);                    // destroys receptor
        }
    }
}

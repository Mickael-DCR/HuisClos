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
                var collectible = hand.GetChild(0).GetComponent<Collectible>();
                var newProp = Instantiate( collectible.Item.ItemPrefabInspect, _parentPivot.transform.position, Quaternion.identity);
                _parentPivot.Target = newProp;
                Destroy(gameObject);
        }
    }
}

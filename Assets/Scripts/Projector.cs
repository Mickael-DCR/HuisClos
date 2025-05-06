using System;
using UnityEngine;

public class Projector : Prop
{
    [SerializeField] private Light _projectorLight;
    private Texture _cookie;

    private void Start()
    {
        
    }

    public override bool Interact()
    {
        base.Interact();
        if (_resolved)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            var item = playerHand.GetChild(0).gameObject.GetComponent<Collectible>();
            
        }
        
        return true;
    }
}

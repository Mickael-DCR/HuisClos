using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectible : Prop
{
    public Item Item;

    public override bool Interact()
    {
        return InventoryManager.Instance.AddItem(Item);
    }
}

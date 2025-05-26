using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectible : MonoBehaviour
{
    public Item Item;

    public bool PickUp()
    {
        GetComponent<PuzzleStepUpdater>()?.UpdateStep();
        SoundManager.instance.PlayTake();
        return InventoryManager.Instance.AddItem(Item, gameObject);
    }
}

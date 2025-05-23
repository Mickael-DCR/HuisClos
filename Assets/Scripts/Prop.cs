using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] protected string[] _solverTags; // Multiple tags for multiple items
    protected bool _resolved;

    public virtual bool Interact()
    {
        return false;
    }

    public virtual void PlaceItem()
    {
        if (_resolved) return;

        
        var playerHand = InventoryManager.Instance.HandSlot;
        if (playerHand.childCount == 0)
        {
            Debug.Log("No items in hand.");
            return;
        }

        Transform objectInHand = playerHand.GetChild(0);

        // Check if the item in hand matches any of the required tags
        foreach (string tag in _solverTags)
        {
            if (objectInHand.CompareTag(tag))
            {
                SoundManager.instance.PlayDrop();
                _resolved = true;
                Debug.Log("Correct item detected.");
                return;
            }
        }

        Debug.Log("No matching item found.");
    }
}
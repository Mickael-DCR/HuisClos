using UnityEngine;

public class Prop : MonoBehaviour
{
    protected bool _resolved;
    [SerializeField] private string _solverTag; // Tag held by solver HandItem
    public virtual bool Interact()
    {
        return false;
    }

    public virtual void PlaceItem()
    {
        if (!_resolved)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            Transform objectInHand = null;
            if (playerHand.childCount > 0)
            {
                objectInHand = playerHand.GetChild(0);
            }
            if (objectInHand != null && objectInHand.gameObject.CompareTag(_solverTag))
            {
                _resolved = true;
            }
            else
            {
                // item doesnt fit.
                Debug.Log("No solver found");
            }
        }
    }
    
}

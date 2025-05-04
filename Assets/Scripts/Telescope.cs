using UnityEditor.UIElements;
using UnityEngine;

public class Telescope : Prop
{
    [SerializeField] private GameObject _handSlot;
    [SerializeField] private string _solverTag; // Tag held by solver HandItem
    private bool _resolved;
    
    public override bool Interact()
    {
        UIManager.Instance.ToggleTelescope(true);
        return true;
    }

    public override void PlaceItem()
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
                UIManager.Instance.TelescopeMissingPiece.SetActive(true);
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

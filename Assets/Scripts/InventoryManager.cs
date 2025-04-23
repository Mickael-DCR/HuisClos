using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryWindow;
    private InputAction _toggleInventoryAction;
    private InputSystem_Actions _inputSystemActions;
    private void Awake()
    {
        _inputSystemActions= new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _toggleInventoryAction = _inputSystemActions.Player.ToggleInventory;
        _toggleInventoryAction.Enable();
        _toggleInventoryAction.performed += ToggleInventory;
    }

    private void OnDisable()
    {
        _toggleInventoryAction.Disable();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (_inventoryWindow.activeInHierarchy) _inventoryWindow.SetActive(false);
        else _inventoryWindow.SetActive(true);
    }
    
    
}

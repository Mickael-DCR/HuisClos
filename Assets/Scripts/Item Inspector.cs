using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class ItemInspector : MonoBehaviour
{
    
    public static ItemInspector Instance;
    public GameObject InspectWindow, InventoryWindow, PlayerCamera;
    public Transform InspectSlot;
    private GameObject _itemPrefab;
    
    [SerializeField] private float rotationSpeed = 5f;
    private bool isDragging;
    private Vector2 lastMousePosition;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();

            if (!isDragging)
            {
                lastMousePosition = currentMousePosition;
                isDragging = true;
            }

            Vector2 delta = currentMousePosition - lastMousePosition;
            lastMousePosition = currentMousePosition;

            float rotX = delta.y * rotationSpeed * Time.deltaTime;
            float rotY = -delta.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(InspectWindow.transform.up, rotY, Space.World);
            transform.Rotate(InspectWindow.transform.right, rotX, Space.World);
        }
        else
        {
            isDragging = false;
        }
    }
    
}

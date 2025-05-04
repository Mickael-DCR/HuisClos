using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

            transform.Rotate(Camera.main.transform.up, rotY, Space.World);
            transform.Rotate(Camera.main.transform.right, rotX, Space.World);
            
            // Prevents Z axis rotation
            Vector3 euler = transform.eulerAngles;
            euler.z = 0f;
            transform.eulerAngles = euler;
        }
        else
        {
            isDragging = false;
        }
    }
}

using System;
using UnityEngine;

public class ItemInspector : MonoBehaviour
{
    public static ItemInspector Instance;
    public GameObject InspectWindow;
    public Transform InspectSlot;
    private GameObject _itemPrefab;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Interact() // Some items will have triggers, use function on raycast
    {
        
    }
    
    private void Rotate() // drag/directional inputs
    {
        
    }
}

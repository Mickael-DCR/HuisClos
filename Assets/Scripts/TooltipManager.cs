using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    [SerializeField] private Camera playerCamera;
    private float maxDistance = 2.5f;
    
    [SerializeField] private GameObject _tooltipUI;             // UI panel (standard UI panel)
    [SerializeField] private Transform _contentPanel;           // Parent for text options
    [SerializeField] private GameObject _optionPrefab;          // Prefab for each option (TMP_Text)
    
    private TooltipTarget _currentTarget;
    private int _selectedIndex = 0;

    private InputSystem_Actions _inputSystem;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        if (PlayerController.InputSystemActions != null)
        {
            PlayerController.InputSystemActions.Player.Interact.performed += OnInteract;
        }
    }

    private void OnEnable()
    {
        PlayerController.InputSystemActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        PlayerController.InputSystemActions.Player.Interact.Disable();
    }

    void Update()
    {
        if (UIManager.Instance.IsInUI)
        {
            _currentTarget = null;
            _tooltipUI.SetActive(false);
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            TooltipTarget target = hit.collider.GetComponent<TooltipTarget>();
            if (target != null)
            {
                if (_currentTarget != target)
                {
                    _currentTarget = target;
                    _selectedIndex = 0;
                }

                _tooltipUI.SetActive(true);
                return;
            }
        }

        // Hide UI if not looking at a tooltip target
        _currentTarget = null;
        _tooltipUI.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (_currentTarget != null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                hit.collider.SendMessage(_currentTarget.Options[_selectedIndex]);
            }
        }
    }
}

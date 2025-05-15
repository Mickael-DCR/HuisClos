using System;
using System.Collections.Generic;
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
    private List<TMP_Text> _optionPool = new List<TMP_Text>();  // Object pool for option text

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        if (PlayerController.InputSystemActions != null)
        {
            PlayerController.InputSystemActions.Player.Interact.performed += OnInteract;
            PlayerController.InputSystemActions.Player.AltInteraction.performed += OnAltInteract;
        }

        InitializeOptionPool();
    }

    private void OnEnable()
    {
        PlayerController.InputSystemActions.Player.Interact.Enable();
        PlayerController.InputSystemActions.Player.AltInteraction.Enable();
    }

    private void OnDisable()
    {
        PlayerController.InputSystemActions.Player.Interact.Disable();
        PlayerController.InputSystemActions.Player.AltInteraction.Disable();
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
                    UpdateTooltipUI();
                }

                _tooltipUI.SetActive(true);
                return;
            }
        }

        // Hide UI if not looking at a tooltip target
        _currentTarget = null;
        _tooltipUI.SetActive(false);
    }

    private void InitializeOptionPool()
    {
        _optionPool.Clear();
    
        for (int i = 0; i < 2; i++)  // Only two options
        {
            var optionInstance = Instantiate(_optionPrefab, _contentPanel);
            optionInstance.SetActive(false);
            _optionPool.Add(optionInstance.GetComponentInChildren<TMP_Text>());
        }
    }

    private void UpdateTooltipUI()
    {
        if (_currentTarget == null) return;

        // Deactivate all options first
        foreach (var option in _optionPool)
        {
            option.transform.parent.gameObject.SetActive(false); // Disable entire option prefab
        }

        // Display first option if available
        if (_currentTarget.Options.Count > 0 && _currentTarget.OptionsName.Count > 0)
        {
            _optionPool[0].text = _currentTarget.OptionsName[0];
            _optionPool[0].transform.parent.gameObject.SetActive(true); // Enable the parent (Image)
        }

        // Display second option if available
        if (_currentTarget.Options.Count > 1 && _currentTarget.OptionsName.Count > 1)
        {
            _optionPool[1].text = _currentTarget.OptionsName[1];
            _optionPool[1].transform.parent.gameObject.SetActive(true); // Enable the parent (Image)
        }

        // Ensure UI is updated
        LayoutRebuilder.ForceRebuildLayoutImmediate(_contentPanel.GetComponent<RectTransform>());
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (_currentTarget != null && _currentTarget.Options.Count > 0)
        {
            SendMessageToTarget(0); // First option (E / LMB)
        }
    }

    private void OnAltInteract(InputAction.CallbackContext ctx)
    {
        if (_currentTarget != null && _currentTarget.Options.Count > 1)
        {
            SendMessageToTarget(1); // Second option (A / RMB)
        }
    }

    private void SendMessageToTarget(int index)
    {
        if (_currentTarget == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (index < _currentTarget.Options.Count)
            {
                Debug.Log($"TooltipManager: Sending message - {_currentTarget.Options[index]}");
                hit.collider.SendMessage(_currentTarget.Options[index]);
            }
        }
    }
}

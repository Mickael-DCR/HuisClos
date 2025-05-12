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
    private Vector2 _scroll;

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

        _scroll = PlayerController.InputSystemActions.Player.Tooltip.ReadValue<Vector2>();
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
                    PopulateOptions();
                }

                _tooltipUI.SetActive(true);
                HandleScrollInput();
                UpdateOptionHighlight();
                return;
            }
        }

        // Hide UI if not looking at a tooltip target
        _currentTarget = null;
        _tooltipUI.SetActive(false);
    }

    private void PopulateOptions()
    {
        // Clear existing options
        foreach (Transform child in _contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Populate new options
        foreach (string option in _currentTarget.Options)
        {
            GameObject newOption = Instantiate(_optionPrefab, _contentPanel);
            newOption.GetComponent<TMP_Text>().text = option;
        }
    }

    private void HandleScrollInput()
    {
        if (_scroll.y > 0f)
            _selectedIndex = (_selectedIndex - 1 + _currentTarget.Options.Count) % _currentTarget.Options.Count;
        else if (_scroll.y < 0f)
            _selectedIndex = (_selectedIndex + 1) % _currentTarget.Options.Count;
    }

    private void UpdateOptionHighlight()
    {
        for (int i = 0; i < _contentPanel.childCount; i++)
        {
            TMP_Text optionText = _contentPanel.GetChild(i).GetComponent<TMP_Text>();
            optionText.color = (i == _selectedIndex) ? Color.yellow : Color.white;
        }
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

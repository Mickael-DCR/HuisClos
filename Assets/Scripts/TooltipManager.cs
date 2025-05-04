using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    [SerializeField] private Camera playerCamera;
    private float maxDistance = 2.5f;
    [SerializeField] private GameObject _tooltipUI;             // UI panel
    [SerializeField] private TMP_Text _tooltipText;             // Text component

    private TooltipTarget _currentTarget;
    private int _selectedIndex = 0;
    
    private InputSystem_Actions _inputSystem;
    private Vector2 _scroll;

    private void Awake()
    {
        if(Instance ==null) Instance = this;
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
                }

                _tooltipUI.SetActive(true);
                _tooltipText.text = $"<b>{_currentTarget.Options[_selectedIndex]}</b>\n(Scroll to change, Press T to select)";

                HandleScrollInput(_currentTarget);
                return;
            }
        }

        // Hide UI if not looking at a tooltip target
        _currentTarget = null;
        _tooltipUI.SetActive(false);
    }

    public void HandleScrollInput(TooltipTarget target)
    {
        
        if (_scroll.y > 0f)
            _selectedIndex = (_selectedIndex + 1) % target.Options.Count;
        else if (_scroll.y < 0f)
            _selectedIndex = (_selectedIndex - 1 + target.Options.Count) % target.Options.Count;
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

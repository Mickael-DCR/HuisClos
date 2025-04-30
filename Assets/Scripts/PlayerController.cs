using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [Header ("Input")] 
    private InputSystem_Actions _inputSystemActions;
    private InputAction _movementAction, _crouchAction, _lookAction, _interactAction, _openInventoryAction, _closeInventoryAction;
    private Vector2 _moveDirection, _lookDirection;
    
    [Header ("Player Movement")]
    [SerializeField] private float _speed;
    private Rigidbody _playerRB;

    [Header("Crouch Settings")] 
    [SerializeField] private float _crouchHeight = 1f;
    [SerializeField] private float _standingHeight = 2f;
    [SerializeField] private float _crouchCamOffset = -0.5f;
    [SerializeField] private float _crouchDuration = 0.15f;
    [SerializeField] private float _crouchSpeed = 0.15f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    private Coroutine _crouchRoutine;
    private CapsuleCollider _capsuleCollider;
    private bool _isCrouching, _isGrounded;
    private Vector3 _originalCameraLocalPosition;
    
    [Header ("Camera Movement")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _lookSensitivity;
    private float _xRotation, _yRotation, _lookAngle;
    private Vector3 _camForward, _camRight;
    
    
    [Header ("Interaction Settings")]
    [SerializeField] private float _interactionRange = 3f;
    [SerializeField] private LayerMask _pickupLayer;
    
    [Header ("Interaction Settings")]
    [SerializeField] private GameObject _crosshair;
    
    
    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
        _playerRB = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _originalCameraLocalPosition = _cameraTransform.localPosition;
    }

    private void OnEnable()
    {
        _movementAction = _inputSystemActions.Player.Move;
        _movementAction.Enable();
        
        _lookAction = _inputSystemActions.Player.Look;
        _lookAction.Enable();
        
        _crouchAction = _inputSystemActions.Player.Crouch;
        _crouchAction.Enable();
        _crouchAction.performed += StartCrouch;
        
        _interactAction = _inputSystemActions.Player.Interact;
        _interactAction.Enable();
        _interactAction.performed += Interact;
        
        _openInventoryAction = _inputSystemActions.Player.OpenInventory;
        _openInventoryAction.Enable();
        _openInventoryAction.performed += OpenInventoryWindow;
        
        _closeInventoryAction = _inputSystemActions.UI.CloseInventory;
        _closeInventoryAction.performed += CloseInventoryWindow;
    }

    private void OnDisable()
    {
        _movementAction.Disable();
        _lookAction.Disable();
        _crouchAction.Disable();
        _interactAction.Disable();
        _openInventoryAction.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        InputResfresher();
        LookAround();
        
        // Debugs
        Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _interactionRange, Color.red);
        Debug.DrawRay(transform.position, Vector3.up * _crouchHeight, Color.green);
        
    }
    

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    
    private void StartCrouch(InputAction.CallbackContext context)
    {
        if (_isCrouching && !_isGrounded)
        {
            // roof check
            if (Physics.Raycast(transform.position, _cameraTransform.up * _crouchHeight, _standingHeight - _crouchHeight)) return;
        
            _isCrouching = false;

            if (_crouchRoutine != null) StopCoroutine(_crouchRoutine);
            _crouchRoutine = StartCoroutine(CrouchLerpRoutine(
                _capsuleCollider.height,
                _standingHeight,
                transform.position,
                transform.position + Vector3.up * ((_standingHeight - _crouchHeight) / 2f),
                _cameraTransform.localPosition,
                _originalCameraLocalPosition
            ));
        }
        else
        {
            _isCrouching = true;

            if (_crouchRoutine != null) StopCoroutine(_crouchRoutine);
            _crouchRoutine = StartCoroutine(CrouchLerpRoutine(
                _capsuleCollider.height,
                _crouchHeight,
                transform.position,
                transform.position - Vector3.up * ((_standingHeight - _crouchHeight) / 2f),
                _cameraTransform.localPosition,
                _originalCameraLocalPosition + new Vector3(0, _crouchCamOffset, 0)
            ));
        }
    }
    
    private IEnumerator CrouchLerpRoutine(
        float fromHeight, float toHeight,
        Vector3 fromPos, Vector3 toPos,
        Vector3 camFrom, Vector3 camTo)
    {
        float elapsed = 0f;

        while (elapsed < _crouchDuration)
        {
            float t = elapsed / _crouchDuration;
            _capsuleCollider.height = Mathf.Lerp(fromHeight, toHeight, t);
            transform.position = Vector3.Lerp(fromPos, toPos, t);
            _cameraTransform.localPosition = Vector3.Lerp(camFrom, camTo, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Final snap (avoids drift)
        _capsuleCollider.height = toHeight;
        transform.position = toPos;
        _cameraTransform.localPosition = camTo;
    }
    
    private void InputResfresher()
    {
        _moveDirection = _movementAction.ReadValue<Vector2>();
        _lookDirection = _lookAction.ReadValue<Vector2>();
        _camForward = _cameraTransform.forward;
        _camRight = _cameraTransform.right;
        //_isGrounded=Physics.Raycast(_groundCheck.position, -_groundCheck.up, 0.1f);
    }
    
    
    private void LookAround()
    {
        _yRotation += _lookDirection.x * _lookSensitivity;
        _xRotation -= _lookDirection.y * _lookSensitivity;
        // So that the camera can't backflip
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
    
    private void MovePlayer()
    {
    // Can't fly when looking up sorry
    _camForward.y = 0f;
    _camRight.y = 0f;
    // speed safety purposes
    _camForward.Normalize();
    _camRight.Normalize();
    Vector3 move = (_camForward * _moveDirection.y + _camRight * _moveDirection.x);
    move *= !_isCrouching ? _speed : _crouchSpeed;
    //Allows Jumping
    move.y = _playerRB.linearVelocity.y; 
    _playerRB.linearVelocity = move;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        
        //For Collectible Objects
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange, _pickupLayer))
        {
            var pickup = hit.collider.GetComponent<Collectible>();
            if (pickup != null)
            {
                if (pickup.Interact())
                {
                    //Debug
                    Debug.Log("Interact");
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void CloseInventoryWindow(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.InventoryWindow.SetActive(false);
        _crosshair.SetActive(true);
        UIManager.Instance.ToggleCursor();
        // Input changes
        _inputSystemActions.UI.Disable();
        _inputSystemActions.Player.Enable();
    }

    private void OpenInventoryWindow(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.InventoryWindow.SetActive(true); // Opens Inventory
        _crosshair.SetActive(false);
        UIManager.Instance.ToggleCursor();
        // Input changes
        _inputSystemActions.Player.Disable();
        _inputSystemActions.UI.Enable();
    }

    
}

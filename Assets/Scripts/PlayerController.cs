using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [Header ("Input")]
    private InputSystem_Actions _inputSystemActions;
    private InputAction _movementAction, _crouchAction, _lookAction, _jumpAction, _interactAction;
    private Vector2 _moveDirection, _lookDirection;
    
    [Header ("Player Movement")]
    [SerializeField] private float _speed, _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    private Rigidbody _playerRB;
    private bool _isGrounded;
    
    [Header("Crouch Settings")]
    [SerializeField] private float _crouchHeight = 1f, _standingHeight = 2f, _crouchCamOffset = -0.5f, _crouchDuration = 0.15f;
    private Coroutine _crouchRoutine;
    private CapsuleCollider _capsuleCollider;
    private bool _isCrouching;
    private Vector3 _originalCameraLocalPosition;
    
    [Header ("Camera Movement")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _lookSensitivity;
    private float _xRotation, _yRotation, _lookAngle;
    private Vector3 _camForward, _camRight;
    
    
    

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
        _inputSystemActions.Player.Jump.performed +=  JustJumpAlready;
        _inputSystemActions.Player.Jump.Enable();
        _crouchAction = _inputSystemActions.Player.Crouch;
        _crouchAction.Enable();
        _crouchAction.performed += StartCrouch;
        _crouchAction.canceled += StopCrouch;
    }

    private void OnDisable()
    {
        _movementAction.Disable();
        _lookAction.Disable();
        _inputSystemActions.Player.Jump.Disable();
        _crouchAction.Disable();
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
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void JustJumpAlready(InputAction.CallbackContext context) // OnJump() already used
    {
        if(_isGrounded)
            _playerRB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    private void StartCrouch(InputAction.CallbackContext context)
    {
        if (_isCrouching) return;
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

    private void StopCrouch(InputAction.CallbackContext context)
    {
        if (!_isCrouching) return;

        Vector3 origin = transform.position + Vector3.up * _crouchHeight;
        if (Physics.Raycast(origin, Vector3.up, _standingHeight - _crouchHeight, _groundLayer)) return;

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

        // Final snap (avoid drift)
        _capsuleCollider.height = toHeight;
        transform.position = toPos;
        _cameraTransform.localPosition = camTo;
    }
    
    private void InputResfresher()
    {
        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, 0.1f, _groundLayer);
        _moveDirection = _movementAction.ReadValue<Vector2>();
        _lookDirection = _lookAction.ReadValue<Vector2>();
        _camForward = _cameraTransform.forward;
        _camRight = _cameraTransform.right;
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
    move *= _speed;
    //Allows Jumping
    move.y = _playerRB.linearVelocity.y; 
    _playerRB.linearVelocity = move;
    }
    
}

using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _inputSystemActions;
    [SerializeField] private float _speed, _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    private InputAction _movementAction, _crouchAction, _lookAction, _jumpAction, _interactAction;
    private Rigidbody _playerRB;
    private bool _isGrounded;

    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
        _playerRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _movementAction = _inputSystemActions.Player.Move;
        _movementAction.Enable();
        _lookAction = _inputSystemActions.Player.Look;
        _lookAction.Enable();
        _inputSystemActions.Player.Jump.performed += JustJumpAlready;
        _inputSystemActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _movementAction.Disable();
        _lookAction.Disable();
        _inputSystemActions.Player.Jump.Disable();
    }

    private void JustJumpAlready(InputAction.CallbackContext context)
    {
        if(_isGrounded)
            _playerRB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, 0.1f, _groundLayer);
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = _movementAction.ReadValue<Vector2>();
        Vector2 lookDirection = _lookAction.ReadValue<Vector2>();
        Vector3 velocity = _playerRB.linearVelocity;
        velocity.z = moveDirection.y*_speed;
        velocity.x = moveDirection.x*_speed;
        _playerRB.linearVelocity = velocity;
    }
    
}

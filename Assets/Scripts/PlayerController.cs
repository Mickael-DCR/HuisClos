using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _moveVector;
    private float _lookAngle;
    private InputAction _movementAction, _crouchAction, _lookAction, _jumpAction, _interactAction;
    private Rigidbody _playerRB;

    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _movementAction = InputSystem.actions.FindAction("Move");
        _crouchAction = InputSystem.actions.FindAction("Crouch");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        _moveVector = _movementAction.ReadValue<Vector2>();
        _lookAngle = _lookAction.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        if (_moveVector.x != 0 || _moveVector.y != 0)
        {
            _playerRB.linearVelocity = new Vector3(_moveVector.x, 0, _moveVector.y)*_speed;
        }
        
    }
    
}

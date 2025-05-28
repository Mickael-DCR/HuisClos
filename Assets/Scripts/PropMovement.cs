using UnityEngine;
using UnityEngine.Serialization;

public class PropMovement : MonoBehaviour
{
    public enum InteractionType { Move, Rotate }
    [SerializeField] private InteractionType _interactionType = InteractionType.Move;

    [SerializeField] private bool _needsKey;
    [SerializeField] private string _keyTag;
    [Header("Movement Settings")]
    [SerializeField] private Vector3 _moveOffset = new Vector3(0, 0, 1); // How far to move
    [SerializeField] private float _moveSpeed = 2f;

    [Header("Rotation Settings")]
    [SerializeField] private Vector3 _rotationAxis = Vector3.up; // Axis of rotation
    [SerializeField] private float _rotationAngle = 90f;
    [SerializeField] private float _rotationSpeed = 100f;
    
    [Header("FX Settings")]
    [SerializeField] private int _propToFxIndex = 0;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _isInteracting = false;
    private bool _isOpen = false;
    
    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public void Open()
    {
        Interact();
        FxManager.Instance.PlayFx(_propToFxIndex);
    }
    public void Interact()
    {
        if (_isInteracting) return;
        if (_needsKey)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            if (playerHand.childCount == 0) return;

            Transform objectInHand = playerHand.GetChild(0);
            Item itemToRemove = objectInHand.GetComponent<Collectible>().Item;
            
            InventoryManager.Instance.RemoveItem(itemToRemove);
            return;
        }
        _isInteracting = true;
        _isOpen = !_isOpen;

        if (_interactionType == InteractionType.Move)
            StartCoroutine(MoveCoroutine());
        else if (_interactionType == InteractionType.Rotate)
            StartCoroutine(RotateCoroutine());
    }
    

    private System.Collections.IEnumerator MoveCoroutine()
    {
        Vector3 targetPosition = _isOpen ? _initialPosition + _moveOffset : _initialPosition;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        transform.position = targetPosition;
        _isInteracting = false;
    }

    private System.Collections.IEnumerator RotateCoroutine()
    {
        Quaternion targetRotation = _isOpen ? _initialRotation * Quaternion.Euler(_rotationAxis * _rotationAngle) : _initialRotation;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRotation;
        _isInteracting = false;
    }

}

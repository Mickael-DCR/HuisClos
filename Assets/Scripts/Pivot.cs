using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Pivot : Prop
{
    public EmissiveChanger EmissiveTarget;
    public bool IsEmissive;
    
    // Object to rotate 
    public GameObject Target;
    
    // Rotation Axis (Vector3)
    public Vector3 AxeRotation = Vector3.up;
    public float RotationAngle = 60f;
    public float AngleTarget = 120f;

    public bool WinCondition;
    public bool FirstTimeSpawned;
    public bool CanRotate = true;
    [SerializeField] private bool _selfLocking;
    private bool _isLocked = false; // New: Lock flag

    private void Update()
    {
        if (IsEmissive && FirstTimeSpawned)
        {
            DisableEmissive();
            FirstTimeSpawned = false;
        }
    }

    public void DisableEmissive()
    {
        EmissiveTarget.DisableEmissive();
    }

    public override bool Interact()
    {
        if (_isLocked) return false; // New: Prevent interaction if locked

        if (CanRotate)
        {
            // Calculate rotation
            Quaternion currentRotation = Target.transform.localRotation;
            Quaternion targetRotation = currentRotation * Quaternion.Euler(AxeRotation * RotationAngle);

            Target.transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, RotationAngle);
        }
        
        RotationCheck();
        CanRotate = !(WinCondition && _selfLocking) && !_isLocked;
        return true;
    }

    private void RotationCheck()
    {
        // Adjusted to allow for any axis (x, y, z)
        float targetAngle = AngleTarget % 360f;
        float currentAngle = Target.transform.localRotation.eulerAngles.y % 360f; // Assuming Y-axis

        if (Mathf.Abs(currentAngle - targetAngle) < 0.01f)
        {
            WinCondition = true;
        }
        else
        {
            WinCondition = false;
        }
    }

    public void Lock()
    {
        _isLocked = true;
        CanRotate = false;
    }
}

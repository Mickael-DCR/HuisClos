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
    private enum RotationAxis { XAxis, YAxis, ZAxis };
    [SerializeField] private RotationAxis _rotationAxis;
    public Vector3 RotationAxisVector;
    public float RotationAngle = 60f;
    public float AngleTarget = 120f;

    public bool WinCondition;
    public bool FirstTimeSpawned;
    public bool CanRotate = true;
    [SerializeField] private bool _selfLocking;
    private bool _isLocked = false; // Lock flag

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
        if (_isLocked) return false; // Prevents interaction if locked
        
        // Calculate rotation
        Target.transform.Rotate(RotationAxisVector, RotationAngle);
        
        // Check if the object is at the target angle
        RotationCheck();
        return true;
    }

    private void RotationCheck()
    {
        // Get the current rotation in local space
        Quaternion currentRotation = Target.transform.localRotation;

        // Calculate the target rotation based on the specified axis
        Quaternion targetRotation = Quaternion.identity;
        switch (_rotationAxis)
        {
            case RotationAxis.XAxis:
                targetRotation = Quaternion.Euler(AngleTarget, 0f, 0f);
                break;
            case RotationAxis.YAxis:
                targetRotation = Quaternion.Euler(0f, AngleTarget, 0f);
                break;
            case RotationAxis.ZAxis:
                targetRotation = Quaternion.Euler(0f, 0f, AngleTarget);
                break;
        }

        // Calculate the angle difference
        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);

        // If the angle difference is small enough, we consider it a match
        WinCondition = angleDifference < 0.5f;
    }

    public void Lock()
    {
        _isLocked = true;
    }
}

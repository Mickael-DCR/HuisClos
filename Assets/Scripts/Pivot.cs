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
        /*
        Quaternion currentRotation = Target.transform.localRotation;

        Target.transform.localRotation = Quaternion.RotateTowards(currentRotation,
            currentRotation * Quaternion.Euler(AxeRotation * RotationAngle),
            RotationAngle);
        */
        
        
        Target.transform.Rotate(RotationAxisVector, RotationAngle);
        
        
        RotationCheck();
        return true;
    }

    private void RotationCheck()
    {
        // Adjusted to allow for any axis (x, y, z)
        float targetAngle = AngleTarget % 360f;
        float currentAngle = 0f;
        if(_rotationAxis == RotationAxis.XAxis )
        {
            currentAngle = Target.transform.localRotation.eulerAngles.x % 360f; // Assuming X-axis
        }
        else if (_rotationAxis == RotationAxis.YAxis)
        {
            currentAngle = Target.transform.localRotation.eulerAngles.y % 360f; // Y-axis
        }
        else if (_rotationAxis == RotationAxis.ZAxis)
        {
            currentAngle = Target.transform.localRotation.eulerAngles.z % 360f; // Z-axis
        }


        float E = Mathf.Abs(currentAngle - targetAngle);
        if (E < 0.01f)
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
    }
}

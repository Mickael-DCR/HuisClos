using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Pivot : Prop
{
    public EmissiveChanger EmissiveTarget;
    public bool IsEmissive;
    // Objet à tourner 
    public GameObject Target;

    // axe (Vector 3)
    public Vector3 AxeRotation = Vector3.up;
    public float RotationAngle = 60f;
    public float AngleTarget = 120f;

    public bool WinCondition;
    public bool FirstTimeSpawned;
    public bool CanRotate = true;

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
        if(CanRotate)
        {
            //Calcul de la rotation 
            Quaternion currentRotation = Target.transform.localRotation;
            Quaternion targetRotation = currentRotation * Quaternion.Euler(AxeRotation * RotationAngle);

            Target.transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, RotationAngle);
        }
        
        
        RotationCheck();
        return true;
    }

    private void RotationCheck()
    {
        if( Mathf.Abs(Target.transform.localRotation.eulerAngles.x- AngleTarget) < 0.01f )
        {
            WinCondition = true;
        }
        else
        {
            WinCondition = false;
        }
    }
}

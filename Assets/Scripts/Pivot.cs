using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Pivot : Prop
{
    public EmissiveChanger EmissiveTarget;
    // Objet à tourner 
    public GameObject Target;

    // axe (Vector 3)
    public Vector3 AxeRotation = Vector3.up;
    public float RotationAngle = 60f;

    public float AngleTarget = 120f;

    public bool WinCondition;
    public bool FirstTimeSpawned;

    private void Update()
    {
        if (FirstTimeSpawned)
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
        //Calcul de la rotation 
        Quaternion currentRotation = Target.transform.rotation;
        Quaternion targetRotation = currentRotation * Quaternion.Euler(AxeRotation * RotationAngle);

        Target.transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, RotationAngle );

        RotationCheck();
        return true;
    }

    private void RotationCheck()
    {
        Quaternion targetRot = Quaternion.Euler(AngleTarget, -180, 0);
        float angleDiff = Quaternion.Angle(Target.transform.rotation, targetRot);

        Debug.Log(angleDiff);

        if(angleDiff < 0.1f)
        {
            Debug.Log("Win");
            WinCondition = true;
            
        }
        else
        {
            Debug.Log("Lose");
            WinCondition = false;
            
        }
    }
}

using Unity.Cinemachine;
using UnityEngine;

public class Pivot : Prop
{
    // Objet à tourner 
    public GameObject Target;

    // axe (Vector 3)
    public Vector3 AxeRotation = Vector3.up;
    public float RotationAngle = 60f;

    public float AngleTarget = 120f;

    public bool WinCondition;

    public override bool Interact()
    {
        //var _rotateValue = _axeRotation * _rotationAngle;

        //Calcul de la rotation 
        Quaternion _currentRotation = Target.transform.rotation;
        Quaternion _targetRotation = _currentRotation * Quaternion.Euler(AxeRotation * RotationAngle);

        Target.transform.rotation = Quaternion.RotateTowards(_currentRotation, _targetRotation, RotationAngle );

        Modulo();
        //ici fonction modulo
        return true;
    }

    private void Modulo()
    {
        Quaternion targetRot = Quaternion.Euler(120, 0, 0);
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

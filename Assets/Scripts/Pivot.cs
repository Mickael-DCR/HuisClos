using UnityEngine;

public class Pivot : Prop
{
    // Objet à tourner 
    public GameObject Target;

    // axe (Vector 3)
    public Vector3 _axeRotation = Vector3.up;
    public float _rotationAngle = 60f;
    

    public override bool Interact()
    {
        //var _rotateValue = _axeRotation * _rotationAngle;

        //Calcul de la rotation 
        Quaternion _currentRotation = Target.transform.rotation;
        Quaternion _targetRotation = _currentRotation * Quaternion.Euler(_axeRotation * _rotationAngle);

        Target.transform.rotation = Quaternion.RotateTowards(_currentRotation, _targetRotation, _rotationAngle * Time.deltaTime);
        //Time.deltaTime > pour une rotation indépendente du framerate

        return true;
    }
}

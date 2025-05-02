using Unity.Cinemachine;
using UnityEngine;



public class Pivot : Prop
{
    // Objet à tourner 
    public GameObject Target;

    // axe (Vector 3)
    public Vector3 _axeRotation = Vector3.up;
    public float _rotationAngle = 60f;

    public float _angleTarget = 120f;

    public bool _winCondition = false;

    public override bool Interact()
    {
        //var _rotateValue = _axeRotation * _rotationAngle;

        //Calcul de la rotation 
        Quaternion _currentRotation = Target.transform.rotation;
        Quaternion _targetRotation = _currentRotation * Quaternion.Euler(_axeRotation * _rotationAngle);

        Target.transform.rotation = Quaternion.RotateTowards(_currentRotation, _targetRotation, _rotationAngle );

        Modulo();
        Win();
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
            _winCondition = true;
            
        }
        else
        {
            Debug.Log("Lose");
            _winCondition = false;
            
        }
    }
    // compteur de win condition  

    private void Win()
    {
        if (_winCondition == true)
        {
            Debug.Log("Big Win !");
        }
    }

}

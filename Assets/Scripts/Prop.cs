using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] protected GameObject _propPrefab;
    [SerializeField] protected string _propName;
    

    private void OnHover()
    {
        
    }

    public virtual void Interact()
    {
        
    }
}

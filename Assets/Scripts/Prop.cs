using UnityEngine;

public class Prop : MonoBehaviour
{
    public virtual bool Interact()
    {
        return false;
    }

    public virtual void PlaceItem()
    {
        
    }
}

using UnityEngine;

public class HintPhone : Prop
{
    public override bool Interact()
    {
        if(HintManager.Instance.CanUse)
        {
            HintManager.Instance.ShowHint();
            HintManager.Instance.CanUse = false;
            return true;
        }
        return false;
    }
}

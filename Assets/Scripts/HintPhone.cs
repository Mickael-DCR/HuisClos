using UnityEngine;

public class HintPhone : Prop
{
    public override bool Interact()
    {
        if(HintManager.Instance.CanUse)
        {
            HintManager.Instance.ShowHint();
            HintManager.Instance.CanUse = false;
            HintManager.Instance.StartHintRoutine();
            return true;
        }
        return false;
    }
}

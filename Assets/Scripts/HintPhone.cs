using UnityEngine;

public class HintPhone : Prop
{
    public override bool Interact()
    {
        if (HintManager.Instance.CanUse)
        {
            HintManager.Instance.ShowHintSelectionUI();
            SoundManager.instance.PlayGear();
            return true;
        }
        return false;
    }
}
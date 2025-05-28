using UnityEngine;

public class HintPhone : Prop
{
    public override bool Interact()
    {
        if (HintManager.Instance.CanUse)
        {
            HintManager.Instance.ShowHintSelectionUI();
            SoundManager.instance.PlayGear();
            
            UIManager.Instance.ToggleCursor();
            
            PlayerController.InputSystemActions.Player.Disable();
            PlayerController.InputSystemActions.UI.Enable();
            return true;
        }
        return false;
    }
}
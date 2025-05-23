using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class WinCondition : MonoBehaviour
{
    public enum CheckMode { Automatic, Manual }
    public CheckMode Mode = CheckMode.Automatic; // Choose mode in the inspector
    public enum CheckType { Pivot, Receiver  }
    public CheckType Type = CheckType.Pivot; // Choose type in the inspector
    
    public List<Pivot> Pivots;
    public List<ItemsReceptor> ItemsReceptors;
    
    [SerializeField] private PropMovement _propToMove;
    public bool PropHasMoved = false;

   
   
    private void Update()
    {
        if (Mode == CheckMode.Automatic)
        {
            CheckWinConditions();
        }
    }

    // Called automatically in automatic mode, or manually in manual mode
    public void CheckWinConditions()
    {
        if(Type == CheckType.Pivot)
        {
            int numberOfConditions = Pivots.Count;
            int numberOfTrue = 0;

            foreach (var pivot in Pivots)
            {
                if (pivot.WinCondition) numberOfTrue++;
            }

            if (numberOfTrue == numberOfConditions)
            {
                
                if (!PropHasMoved)
                {
                    LockPivots();
                    PropHasMoved = true;
                    _propToMove.Open();
                }
            }
        }
        else if (Type == CheckType.Receiver)
        {
            int numberOfConditions = ItemsReceptors.Count;
            int numberOfTrue = 0;

            foreach (var receptor in ItemsReceptors)
            {
                if(receptor.RewardActive) numberOfTrue++;
            }
            if(numberOfTrue == numberOfConditions)
            {
                if (!PropHasMoved)
                {
                    PropHasMoved = true;
                    _propToMove.Open();

                }
            }
        }
    }

    private void LockPivots()
    {
        foreach (var pivot in Pivots)
        {
            if(pivot.IsEmissive)
            {
                pivot.EmissiveTarget.EnableEmissive();
                pivot.EmissiveTarget.ChangeEmissiveIntensity();
            }
            pivot.Lock();
            SoundManager.instance.PlayGearWin();
            //FxManager.Instance.Fireworks();
        }
    }

    // New: Method to manually trigger the check (useful for Manual mode)
    public void Interact()
    {
        if (Mode == CheckMode.Manual)
        {
            CheckWinConditions();
        }
    }
}
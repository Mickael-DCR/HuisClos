using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinCondition : MonoBehaviour
{
    public enum CheckMode { Automatic, Manual }
    public CheckMode Mode = CheckMode.Automatic; // Choose mode in the inspector

    public List<Pivot> Pivots;

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
        int numberOfConditions = Pivots.Count;
        int numberOfTrue = 0;
        
        foreach (var pivot in Pivots)
        {
            if (pivot.WinCondition) numberOfTrue++;
        }

        if (numberOfTrue == numberOfConditions)
        {
            LockPivots();
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
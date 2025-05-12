using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinCondition : MonoBehaviour
{
    public List<Pivot> Pivots;
    
    private void Interact()
    {
        var numberOfConditions = Pivots.Count;
        var numberOfTrue = 0;
        foreach (var pivot in Pivots)
        {
            if (pivot.WinCondition) numberOfTrue++;
            
        }

        if (numberOfTrue == numberOfConditions)
        {
            // light up the pivots
            foreach (var pivot in Pivots)
            {
                pivot.EmissiveTarget.EnableEmissive();
                pivot.EmissiveTarget.ChangeEmissiveIntensity(); 
                pivot.CanRotate = false;
            }
            
        }
    }
}

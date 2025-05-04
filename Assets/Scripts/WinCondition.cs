using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinCondition : MonoBehaviour
{
    public List<Pivot> Conditions;
    
    private void Interact()
    {
        var numberOfConditions = Conditions.Count;
        var numberOfTrue = 0;
        foreach (var win in Conditions)
        {
            if (win.WinCondition) numberOfTrue++;
        }

        if (numberOfTrue == numberOfConditions)
        {
            Debug.Log("Win");
            // 
        }
    }
}

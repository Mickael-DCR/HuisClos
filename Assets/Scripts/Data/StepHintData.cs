using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepHintData
{
    [TextArea]
    public List<string> hints;  // Multiple hints per step
}
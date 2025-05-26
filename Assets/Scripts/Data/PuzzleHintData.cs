using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzleHintData", menuName = "Puzzle Hint", order = 1)]
public class PuzzleHintData : ScriptableObject
{
    public string puzzleName;
    public List<StepHintData> steps; // One entry per puzzle step
}
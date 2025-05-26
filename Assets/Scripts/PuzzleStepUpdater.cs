using UnityEngine;

public class PuzzleStepUpdater : MonoBehaviour
{
    [Header("Puzzle Step Configuration")]
    public string puzzleName;
    public int stepToSet;
    public bool onlyAdvance = true;


    private bool _hasUpdated = false;

    public void UpdateStep()
    {
        if (_hasUpdated) return;

        int currentStep = HintManager.Instance.GetCurrentStep(puzzleName);
        
        // Checks if the step we are setting is after the current step
        if (!onlyAdvance || stepToSet > currentStep)
        {
            HintManager.Instance.SetCurrentStep(puzzleName, stepToSet);
            
            _hasUpdated = true;
        }
    }
}
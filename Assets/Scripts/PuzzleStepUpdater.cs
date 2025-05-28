using UnityEngine;
using UnityEngine.Serialization;

public class PuzzleStepUpdater : MonoBehaviour
{
    [Header("Puzzle Step Configuration")]
    [SerializeField] private string _puzzleName;
    [SerializeField] private int _stepToSet;
    [SerializeField] private bool _onlyAdvance = true;
    [SerializeField] private bool _updateOnPickUp;
    [SerializeField] private bool _updateOnInteract;
    [SerializeField] private bool _updateOnPlaceItem;

    [SerializeField] private bool _hasUpdated = false;

    public void UpdateStep()
    {
        if (_hasUpdated) return;

        int currentStep = HintManager.Instance.GetCurrentStep(_puzzleName);
        
        // Checks if the step we are setting is after the current step
        if (!_onlyAdvance || _stepToSet > currentStep)
        {
            HintManager.Instance.SetCurrentStep(_puzzleName, _stepToSet);
            
            _hasUpdated = true;
        }
    }

    public void PickUp()
    {
        if (_updateOnPickUp) UpdateStep();
    }

    public void Interact()
    {
        if (_updateOnInteract) UpdateStep();
    }

    public void PlaceItem()
    {
        if (_updateOnPlaceItem) UpdateStep();
    }
}
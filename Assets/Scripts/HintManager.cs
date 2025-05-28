using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;

    [Header("UI References")]
    [SerializeField] private TMP_Text _hintTextUI;
    [SerializeField] private GameObject _hintPanel;
    public GameObject HintSelectorPanel;
    [SerializeField] private Button _puzzle1Button;
    [SerializeField] private Button _puzzle2Button;
    [SerializeField] private Button _puzzle3Button;

    [Header("Data")]
    [SerializeField] private List<PuzzleHintData> _allPuzzleHints;

    [Header("Timing")]
    [SerializeField] private float _displayDuration = 5f;
    [SerializeField] private float _intervalBetweenHints = 120f;

    [Header("Dependencies")]
    [SerializeField] private EmissiveChanger _emissiveToChange;

    private Dictionary<string, PuzzleHintData> _hintData;
    private Dictionary<string, int> _stepByPuzzle;
    private Dictionary<string, int> _hintIndexByPuzzle;
    private Coroutine _hintCoroutine;

    public bool CanUse;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // Init puzzle data
        _hintData = new Dictionary<string, PuzzleHintData>();
        _stepByPuzzle = new Dictionary<string, int>();
        _hintIndexByPuzzle = new Dictionary<string, int>();

        foreach (var puzzle in _allPuzzleHints)
        {
            if (puzzle == null) continue;
            _hintData[puzzle.puzzleName] = puzzle;
            _stepByPuzzle[puzzle.puzzleName] = 0;
            _hintIndexByPuzzle[puzzle.puzzleName] = 0;
        }

        // Button listeners
        _puzzle1Button.onClick.AddListener(() => OnPuzzleSelected("Puzzle1"));
        _puzzle2Button.onClick.AddListener(() => OnPuzzleSelected("Puzzle2"));
        _puzzle3Button.onClick.AddListener(() => OnPuzzleSelected("Puzzle3"));

        // Start cooldown coroutine
        _hintCoroutine = StartHintRoutine();
    }

    public Coroutine StartHintRoutine()
    {
        return StartCoroutine(HintRoutine());
    }

    private IEnumerator HintRoutine()
    {
        if (!CanUse)
        {
            _emissiveToChange.DisableEmissive();
            yield return new WaitForSeconds(_intervalBetweenHints);
            _emissiveToChange.EnableEmissive();
            CanUse = true;
        }
        else
        {
            yield return null;
        }
    }

    public void ShowHintSelectionUI()
    {
        HintSelectorPanel.SetActive(true);
    }

    private void OnPuzzleSelected(string puzzleName)
    {
        HintSelectorPanel.SetActive(false);
        ShowHintForPuzzle(puzzleName);
        CanUse = false;
        StartHintRoutine();
    }

    public void ShowHintForPuzzle(string puzzleName)
    {
        UIManager.Instance.ToggleCursor();
            
        PlayerController.InputSystemActions.Player.Enable();
        PlayerController.InputSystemActions.UI.Disable();
        if (!_hintData.ContainsKey(puzzleName)) return;

        var puzzle = _hintData[puzzleName];
        int stepIndex = _stepByPuzzle[puzzleName];

        if (stepIndex >= puzzle.steps.Count) return;

        var stepHints = puzzle.steps[stepIndex].hints;
        if (stepHints == null || stepHints.Count == 0) return;

        int hintIndex = _hintIndexByPuzzle[puzzleName];
        string hint = stepHints[hintIndex];

        _hintTextUI.text = hint;
        _hintTextUI.gameObject.SetActive(true);
        _hintPanel.SetActive(true);

        _hintIndexByPuzzle[puzzleName] = (hintIndex + 1) % stepHints.Count;

        CancelInvoke(nameof(HideHint));
        Invoke(nameof(HideHint), _displayDuration);
    }

    private void HideHint()
    {
        _hintPanel.SetActive(false);
        _hintTextUI.gameObject.SetActive(false);
    }

    public void SetCurrentStep(string puzzleName, int step)
    {
        if (!_hintData.ContainsKey(puzzleName)) return;

        int clampedStep = Mathf.Clamp(step, 0, _hintData[puzzleName].steps.Count - 1);
        _stepByPuzzle[puzzleName] = clampedStep;
        _hintIndexByPuzzle[puzzleName] = 0;
    }
    
    public int GetCurrentStep(string puzzleName)
    {
        if (_stepByPuzzle.ContainsKey(puzzleName))
        {
            return _stepByPuzzle[puzzleName];
        }

        return 0;
    }

    public void AdvanceStep(string puzzleName)
    {
        if (!_hintData.ContainsKey(puzzleName)) return;

        int current = _stepByPuzzle[puzzleName];
        int max = _hintData[puzzleName].steps.Count - 1;

        _stepByPuzzle[puzzleName] = Mathf.Min(current + 1, max);
        _hintIndexByPuzzle[puzzleName] = 0;
    }

    public void ResetHints(string puzzleName)
    {
        if (_stepByPuzzle.ContainsKey(puzzleName))
        {
            _stepByPuzzle[puzzleName] = 0;
            _hintIndexByPuzzle[puzzleName] = 0;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    [Header("Configuration")]
    [SerializeField] private TMP_Text _hintTextUI;               // TMP Text to display hints
    [SerializeField] private GameObject _hintPanel;              // Background for text visibility
    [SerializeField] private float _displayDuration = 5f;        // Duration the hint is displayed (in seconds)
    [SerializeField] private float _intervalBetweenHints = 120f; // Interval between hints (in seconds)

    [Header("Hint Lists (Contextual)")]
    [SerializeField] private List<string> _puzzle1Hints;     // List of hints for Puzzle 1
    [SerializeField] private List<string> _puzzle2Hints;     // List of hints for Puzzle 2
    [SerializeField] private List<string> _puzzle3Hints;     // List of hints for Puzzle 3

    [SerializeField] private EmissiveChanger _emissiveToChange;
    private Dictionary<string, List<string>> _hintDictionary;
    private string _currentPuzzle = "Puzzle1";               // Default puzzle
    private int _currentHintIndex = 0;
    private Coroutine _hintCoroutine;
    public bool CanUse;

    private void Awake()
    {
        if (Instance == null)Instance = this;
    }

    private void Start()
    {
        // Initializing the dictionary of hints by context
        _hintDictionary = new Dictionary<string, List<string>>
        {
            { "Puzzle1", _puzzle1Hints },
            { "Puzzle2", _puzzle2Hints },
            { "Puzzle3", _puzzle3Hints }
        };

        // Start the hint generation process
        _hintCoroutine = StartCoroutine(HintRoutine());
    }

    private IEnumerator HintRoutine()
    {
        while (!CanUse)
        {
            _emissiveToChange.DisableEmissive();
            yield return new WaitForSeconds(_intervalBetweenHints);
            _emissiveToChange.EnableEmissive();
            CanUse = true;
        }
        
    }

    public void ShowHint()
    {
        // Check if the current puzzle has a list of hints
        if (_hintDictionary.ContainsKey(_currentPuzzle) && _hintDictionary[_currentPuzzle].Count > 0)
        {
            CanUse = false;
            _hintPanel.SetActive(true);
            // Display the corresponding hint
            _hintTextUI.text = _hintDictionary[_currentPuzzle][_currentHintIndex];
            _hintTextUI.gameObject.SetActive(true);

            // Advance the index for the next hint
            _currentHintIndex = (_currentHintIndex + 1) % _hintDictionary[_currentPuzzle].Count;

            // Hide the hint after the specified duration
            Invoke(nameof(HideHint), _displayDuration);
        }
    }

    private void HideHint()
    {
        _hintPanel.SetActive(false);
        _hintTextUI.gameObject.SetActive(false);
    }

    // Method to change context (puzzle)
    public void SetCurrentPuzzle(string puzzleName)
    {
        if (_hintDictionary.ContainsKey(puzzleName))
        {
            _currentPuzzle = puzzleName;
            _currentHintIndex = 0; // Reset the hint index
        }
        else
        {
            Debug.LogWarning($"Puzzle '{puzzleName}' not found in the hint list.");
        }
    }

    
    public void StopHints()
    {
        if (_hintCoroutine != null)
        {
            StopCoroutine(_hintCoroutine);
        }
        _hintTextUI.gameObject.SetActive(false);
    }
}

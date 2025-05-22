using System;
using UnityEngine;

public class HintUpdater : MonoBehaviour
{
    [SerializeField] private HintManager _hintManager;
    [SerializeField] private WinCondition _winCondition;
    [SerializeField] private string _puzzleName;
    private bool _isDone = false;

    private void Update()
    {
        if (!_isDone && _winCondition.PropHasMoved)
        {
            _hintManager.SetCurrentPuzzle(_puzzleName);
            _isDone = true;
        }
    }
}

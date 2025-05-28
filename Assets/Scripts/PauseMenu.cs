using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject PauseWindow;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        
        PlayerController.InputSystemActions.Player.Pause.performed += ctx => TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        PauseWindow.SetActive(isPaused);
        Time.timeScale = isPaused ? 0.0f : 1.0f;

        UIManager.Instance.ToggleCursor();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

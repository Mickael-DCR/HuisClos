using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseWindow;
    private bool isPaused = false;

    private InputSystem_Actions _inputAction;

    void Awake()
    {
        _inputAction = new InputSystem_Actions();
        _inputAction.UI.Pause.performed += ctx => TogglePause();
    }

    void OnEnable()
    {
        _inputAction.UI.Enable();
    }

    void OnDisable()
    {
        _inputAction.UI.Disable();
    }

    public void TogglePause()
    {
        //Debug.Log("�a marche");
        isPaused = !isPaused;
        PauseWindow.SetActive(isPaused);
        Time.timeScale = isPaused ? 0.0f : 1.0f;
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}

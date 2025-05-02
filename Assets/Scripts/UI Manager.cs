using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject _crosshair;
    public GameObject TelescopeWindow;
    public GameObject TelescopeMissingPiece;

    private void Awake()
    {
        if(Instance == null)Instance = this;
    }

    public void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _crosshair.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _crosshair.SetActive(true);
        }
    }

    public void ToggleTelescope()
    {
        if (TelescopeWindow.activeInHierarchy)
        {
            TelescopeWindow.SetActive(false);
            PlayerController.InputSystemActions.Player.Enable();
            PlayerController.InputSystemActions.UI.Disable();
        }
        else
        {
            TelescopeWindow.SetActive(true);
            PlayerController.InputSystemActions.Player.Disable();
            PlayerController.InputSystemActions.UI.Enable();
        }
        ToggleCursor();
    }
}

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject Crosshair;
    public GameObject TelescopeWindow, InspectWindow;
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
            Crosshair.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Crosshair.SetActive(true);
        }
    }

    public void ToggleTelescope(bool on)
    {
        if (!on)
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

    public void ToggleInspectWindow(bool on)
    {
        InspectWindow.SetActive(on);
    }
}

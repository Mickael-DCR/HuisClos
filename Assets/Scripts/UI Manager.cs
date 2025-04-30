using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

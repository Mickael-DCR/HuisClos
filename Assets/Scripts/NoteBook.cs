using UnityEngine;
using UnityEngine.InputSystem;

public class NoteBook : MonoBehaviour
{
    public GameObject PageOne;
    public GameObject PageTwo;
    public GameObject PageThree;

    public GameObject TextTelescope;

    public bool IsOpen = false;

    private InputSystem_Actions _inputAction;

    void Awake()
    {
        _inputAction = new InputSystem_Actions();
        _inputAction.UI.Notebook.performed += ctx => Open();
        _inputAction.Enable();
    }
    public void Open()
    {
        IsOpen = !IsOpen;

        PageOne.SetActive(IsOpen);
        PageTwo.SetActive(false);
        PageThree.SetActive(false);

        UIManager.Instance.ToggleCursor();

        if(IsOpen) 
        {
            PlayerController.InputSystemActions.Player.Disable();
            PlayerController.InputSystemActions.UI.Enable();

        }else if (!IsOpen)
        {
            PlayerController.InputSystemActions.Player.Enable();
            PlayerController.InputSystemActions.UI.Disable();
        }
    }
    public void Skip1()
    {
        PageOne.SetActive(false);  
        PageTwo.SetActive(true);
    }

    public void Back1()
    {
        PageTwo.SetActive(false);
        PageOne.SetActive(true);
    }

    public void Skip2()
    {
        PageThree.SetActive(true);
        PageTwo.SetActive( false);
    }

    public void Back2()
    {
        PageThree.SetActive(false);
        PageTwo.SetActive(true);
    }
}

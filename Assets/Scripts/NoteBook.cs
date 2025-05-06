using UnityEngine;
using UnityEngine.InputSystem;

public class NoteBook : MonoBehaviour
{
    public GameObject PageOne;
    public GameObject PageTwo;
    public GameObject PageThree;

    private InputSystem_Actions _inputAction;

    void awake()
    {
        _inputAction = new InputSystem_Actions();
        _inputAction.UI.Notebook.performed += ctx => Open();
    }
    public void Open()
    {
        PageOne.SetActive(true);
    }
    void Skip1()
    {
        PageOne.SetActive(false);  
        PageTwo.SetActive(true);
    }

    void Back1()
    {
        PageTwo.SetActive(false);
        PageOne.SetActive(true);
    }

    void Skip2()
    {
        PageThree.SetActive(true);
        PageTwo.SetActive( false);
    }

    void Back2()
    {
        
    }
}

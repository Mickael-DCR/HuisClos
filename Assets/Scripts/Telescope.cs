using UnityEditor.UIElements;
using UnityEngine;

public class Telescope : Prop
{
    public static Telescope Instance;
    [SerializeField] private GameObject _reward;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] NoteBook _notebook;
    [SerializeField] private string text;
    public override bool Interact()
    {
        UIManager.Instance.ToggleTelescope(true);
        return true;
    }

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved)
        {
            _notebook.Text(text);
            UIManager.Instance.TelescopeMissingPiece.SetActive(true);
            Instantiate(_reward,_spawnPoint);
        }
       
    }
}

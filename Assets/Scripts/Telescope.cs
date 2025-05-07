using UnityEditor.UIElements;
using UnityEngine;

public class Telescope : Prop
{
    public static Telescope Instance;
    [SerializeField] private GameObject _reward;
    [SerializeField] private Transform _spawnPoint;
    
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
            //NoteBook.Instance.Text();
            UIManager.Instance.TelescopeMissingPiece.SetActive(true);
            Instantiate(_reward,_spawnPoint);
        }
       
    }
}

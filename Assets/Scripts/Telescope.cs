using UnityEngine;

public class Telescope : Prop
{
    public static Telescope Instance;
    [SerializeField] private GameObject _reward;
    [SerializeField] private Transform _spawnPoint;
<<<<<<< HEAD
    private bool _rewardActive;
=======
    
>>>>>>> Awen
    public override bool Interact()
    {
        UIManager.Instance.ToggleTelescope(true);
        return true;
    }

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved && !_rewardActive)
        {
<<<<<<< HEAD
            
=======
            //NoteBook.Instance.Text();
>>>>>>> Awen
            UIManager.Instance.TelescopeMissingPiece.SetActive(true);
            Instantiate(_reward,_spawnPoint);
            _rewardActive = true;
        }
       
    }
}

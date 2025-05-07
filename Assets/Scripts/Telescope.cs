using UnityEditor.UIElements;
using UnityEngine;

public class Telescope : Prop
{
    [SerializeField] private GameObject _reward;
    [SerializeField] private Transform _spawnPoint;
    private bool _rewardActive;
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
            
            UIManager.Instance.TelescopeMissingPiece.SetActive(true);
            Instantiate(_reward,_spawnPoint);
            _rewardActive = true;
        }
    }
}

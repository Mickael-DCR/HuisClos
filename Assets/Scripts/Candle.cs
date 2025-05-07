using UnityEngine;

public class Candle : Prop
{
    [SerializeField] private GameObject _light;
    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved)
        {
            _light.SetActive(!_light.activeInHierarchy);
            _resolved = false;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectorProp : ItemsReceptor
{
    [SerializeField] private Light _projectorLight;
    [SerializeField] private GameObject _lighting;
    [SerializeField] private Transform _candleReceiver;
    [SerializeField] private Texture _cookie;
    private bool _firstDisk, _secondDisk, _thirdDisk, _complete;
    public List<bool> Disks = new List<bool>();
    
    
    private void Start()
    {
        Disks.Add(_firstDisk);
        Disks.Add(_secondDisk);
        Disks.Add(_thirdDisk);
    }

    private void Update()
    {
        if (_complete && _candleReceiver.childCount > 0)
        {
            _lighting.SetActive(true);
        }
        else
        {
            _lighting.SetActive(false);
        }
    }

    public override void PlaceItem()
    {
        base.PlaceItem();
        if (_resolved && !_complete)
        {
            if (RewardActive)
            {
                _projectorLight.cookie = _cookie;
                _complete = true;
            }
            _resolved = false;
        }
    }
}

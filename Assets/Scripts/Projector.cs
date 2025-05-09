using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projector : Prop
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
        if (!_lighting.activeInHierarchy && _complete && _candleReceiver.childCount > 0)
        {
            _lighting.SetActive(true);
        }
    }

    public override void PlaceItem()
    {
        if (!_resolved)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            Transform objectInHand = null;
            if (playerHand.childCount > 0)
            {
                objectInHand = playerHand.GetChild(0);
            }

            if (objectInHand != null && objectInHand.gameObject.CompareTag("Candle"))
            {
                _resolved = true;
            }
        }

        base.PlaceItem();
        if (_resolved && !_complete)
        {
            var playerHand = InventoryManager.Instance.HandSlot;
            var item = playerHand.GetChild(0).GetComponent<Disk>();
            Disks[item.ID] = true;
            if (Disks[0] && Disks[1] && Disks[2])
            {
                _complete = true;
                _projectorLight.cookie = _cookie;
            }
            Destroy(playerHand.GetChild(0).gameObject);
            _resolved = false;
        }
    }
}

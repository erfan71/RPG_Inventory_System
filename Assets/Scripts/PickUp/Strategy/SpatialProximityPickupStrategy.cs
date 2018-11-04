using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialProximityPickupStrategy : PickupStrategy
{
    private PlayerMovement _playerMovement;
    void Start()
    {
        PickupableObject.OnPickupableItemClickedEvent += OnPickupableItemClickedCallBack;

        _playerMovement = GetComponentInParent<PlayerMovement>();
    }
    private void OnDestroy()
    {
        PickupableObject.OnPickupableItemClickedEvent -= OnPickupableItemClickedCallBack;

    }

    private void OnPickupableItemClickedCallBack(PickupableObject obj)
    {
        if (Vector2.Distance(obj.GetCenterPosition(), _playerMovement.GetPosition()) < PlayerPickUpHandler.MIN_DISTANCE_TO_PICKUPABLE)
        {
            ItemPickedup(obj);
        }
        else
        {
            FloatingTexts.Instance.Show("Not close enough", FloatingTexts.Type.Warning);
        }
        
    }  
}

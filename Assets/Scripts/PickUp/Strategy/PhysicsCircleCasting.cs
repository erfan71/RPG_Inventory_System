using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCircleCasting : PickupStrategy
{
    private PlayerMovement _playerMovement;
	void Start () {
        _playerMovement = GetComponentInParent<PlayerMovement>();

    }
		void FixedUpdate () {
        RaycastHit2D hit = Physics2D.CircleCast
           (_playerMovement.GetPosition(), PlayerPickUpHandler.OVERLAP_RADIUS, _playerMovement.GetVelocityDirection(),
           PlayerPickUpHandler.CIRCLE_CAST_DISTANCE, LayerMask.GetMask(PlayerPickUpHandler.PICKUPABLE_ITEM_LAYER));
        if (hit)
        {
            Transform hitTrans = hit.transform;
            if (hitTrans.tag == PlayerPickUpHandler.PICKUPABLE_ITEM_TAG)
                ItemPickedup(hitTrans.GetComponent<PickupableObject>());
        }
    }
}

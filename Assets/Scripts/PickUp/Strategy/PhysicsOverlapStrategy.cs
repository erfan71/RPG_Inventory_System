using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsOverlapStrategy : PickupStrategy
{

    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle
            (transform.position, PlayerPickUpHandler.OVERLAP_RADIUS,
            LayerMask.GetMask(PlayerPickUpHandler.PICKUPABLE_ITEM_LAYER));
        if (hit)
        {
            if (hit.tag==PlayerPickUpHandler.PICKUPABLE_ITEM_TAG)
                ItemPickedup(hit.GetComponent<PickupableObject>());
        }
    }
}

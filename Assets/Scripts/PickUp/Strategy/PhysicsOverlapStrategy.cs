using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsOverlapStrategy : PickupStrategy
{

    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle
            (transform.position, PlayerPickUpStrategyHandler.OVERLAP_RADIUS,
            LayerMask.GetMask(PlayerPickUpStrategyHandler.PICKUPABLE_ITEM_LAYER));
        if (hit)
        {
            if (hit.tag==PlayerPickUpStrategyHandler.PICKUPABLE_ITEM_TAG)
                ItemPickedup(hit.GetComponent<PickupableObject>());
        }
    }
}

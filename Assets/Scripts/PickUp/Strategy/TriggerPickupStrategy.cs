using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPickupStrategy : PickupStrategy {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerPickUpHandler.PICKUPABLE_ITEM_TAG))
        {
            ItemPickedup(collision.GetComponent<PickupableObject>());
        }
    }
  
}

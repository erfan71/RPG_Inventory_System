using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStrategy : MonoBehaviour {

    public System.Action<PickupableObject> OnItemPickedUpCallBack;
    protected void ItemPickedup(PickupableObject pickupable)
    {
        OnItemPickedUpCallBack?.Invoke(pickupable);
    }
}

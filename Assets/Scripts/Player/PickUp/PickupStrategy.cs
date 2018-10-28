using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStrategy : MonoBehaviour {

    public System.Action<PickupableObject> ItemPickedUpCallBack;
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    protected void ItemPickedup(PickupableObject pickupable)
    {
        ItemPickedUpCallBack?.Invoke(pickupable);
        Debug.Log("Item PickedUp: "+pickupable.name);
    }
}

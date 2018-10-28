using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour {

	public Vector2 GetCenterPosition()
    {
        return transform.position;
    }
}

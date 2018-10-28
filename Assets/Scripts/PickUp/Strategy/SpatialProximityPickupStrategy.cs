using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// *This class Implement the Option 1 of Pickuping algorithmes
/// *Option 1: Pick up items by clicking on the item in predefined spatial proximity
/// *The algorithmes suffer from perforamce and It is very slow if the number of PickupableObject gets high/
///  I use a coroutine to prevent from freezing in for loop
/// *There are ways to optimize the code like:
///  Partially limit the number of PickupableObject, for example detect only visible items
/// *Another Way to implement the requested feature is move the distance calculation part to each PickupableObject. 
///  The detect the mouse position and check is there in their proximity or not. if there is a match they'll call the 
///  SpatialProximityPickupStrategy class. 
///  It's not a good way too, becasue we will have ScreenToWorldPoint and distance calculation N times.
///  therefore this way is not a big deal too.
/// *Personnaly I will never choose this way if it's on me.
/// </summary>
public class SpatialProximityPickupStrategy : PickupStrategy
{
    private PickupableObject[] _pickupableObjects;
    void Start()
    {
        _pickupableObjects = GameObject.FindObjectsOfType<PickupableObject>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(FindingFinePickupable(inputWorldPosition));
        }
    }
    IEnumerator FindingFinePickupable( Vector2 inputWorldPosition)
    {
        foreach (PickupableObject pickupable in _pickupableObjects)
        {
            if (Vector2.Distance(pickupable.GetCenterPosition(), inputWorldPosition) < PlayerPickUpStrategyHandler.MIN_DISTANCE_TO_PICKUPABLE)
            {
                ItemPickedup(pickupable);
            }
            yield return null;
        }
    }
}

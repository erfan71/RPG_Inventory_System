using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class PlayerPickUpHandler : MonoBehaviour
{

    public enum PickupStrategyMethod
    {
        TriggerPickupStrategy,
        SpatialProximityPickupStrategy,
        PhysicsOverlapStrategy,
        PhysicsCircleCasting
    }
    public PickupStrategyMethod PickupStrategyMethodName;


    private PickupStrategy _pickupStrategy;
    private PickupStrategyMethod _currentSelectedStrategyName;

    public const string PICKUPABLE_ITEM_TAG = "Pickupable";
    public const float MIN_DISTANCE_TO_PICKUPABLE = 1;
    public const float OVERLAP_RADIUS = 0.5f;
    public const float CIRCLE_CAST_DISTANCE = 0.25f;
    public const string PICKUPABLE_ITEM_LAYER = "PickupableLayer";

    public InventoryController InventoryContrtoller;
    void Start()
    {
        ChangeStrategy(PickupStrategyMethodName);
    }
    private void OnDestroy()
    {
        if (_pickupStrategy.OnItemPickedUpCallBack != null)
            _pickupStrategy.OnItemPickedUpCallBack -= OnItemPickedUpCallBack;

    }
    private void OnItemPickedUpCallBack(PickupableObject obj)
    {
        Debug.Log("Item PickedUp: " + obj.GetItemName());
        InventoryContrtoller.AddToInventory(obj.GetItemReference());
        DestroyImmediate(obj.gameObject);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (_currentSelectedStrategyName != PickupStrategyMethodName)
        {
            ChangeStrategy(PickupStrategyMethodName);
        }
#endif
    }
    void ChangeStrategy(PickupStrategyMethod newStrategyName)
    {
        GameObject targetObject = transform.GetChild(0).gameObject;

        PickupStrategy previousStrategy = targetObject.GetComponent<PickupStrategy>();
        if (previousStrategy != null)
        {
            previousStrategy.OnItemPickedUpCallBack -= OnItemPickedUpCallBack;
            Destroy(previousStrategy);
        }
        Type calcType = Type.GetType(newStrategyName.ToString());
        _pickupStrategy = targetObject.AddComponent(calcType) as PickupStrategy;

        _pickupStrategy.OnItemPickedUpCallBack += OnItemPickedUpCallBack;

        _currentSelectedStrategyName = newStrategyName;
    }
}

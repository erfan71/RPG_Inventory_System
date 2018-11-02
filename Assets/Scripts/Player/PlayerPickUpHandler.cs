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
    public const float OVERLAP_RADIUS = 0.25f;
    public const float CIRCLE_CAST_DISTANCE = 0.25f;
    public const string PICKUPABLE_ITEM_LAYER = "PickupableLayer";
    public const string PICKUPABLE_ITEM_PREFAB_KEY = "PickupableItem";
    private const float PICKUPABLE_ITEM_GENERATION_RADIUS = 5.0f;

    private Vector2 _pickupScale = new Vector2(0.75f, 0.75f);

    private InventoryController InventoryContrtoller;
    private PlayerMovement PlayerMovement;
    public Transform PickupableObjectsParent;
    void Start()
    {
        InventoryContrtoller = GetComponent<InventoryController>();
        PlayerMovement = GetComponent<PlayerMovement>();

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

        Item item = obj.GetItemReference();
        if (item.PickupType == Item.PickUpType.Pickupable)
            InventoryContrtoller.AddToInventory(item, true);
        else
        {
            Debug.Log("It's a Permanent Usage Item");
            InventoryContrtoller.ConsumeItem(item);
        }
        DestroyImmediate(obj.gameObject);
    }
    
    public void CreatePickupableItem(Item item, float distance = 1)
    {
        PickupableObject tempPickupable = ObjectPoolManager.Instance.GetObject<PickupableObject>(PICKUPABLE_ITEM_PREFAB_KEY);
        tempPickupable.ItemId = item.Id;
        tempPickupable.transform.position = PlayerMovement.GetPosition();

        Vector2 randomePoint = UnityEngine.Random.insideUnitCircle * distance;

        while(randomePoint.magnitude < 0.9f)
        {
            randomePoint = UnityEngine.Random.insideUnitCircle * distance;
        }

        tempPickupable.transform.Translate(randomePoint);
        tempPickupable.transform.parent = PickupableObjectsParent;
        tempPickupable.transform.localScale = _pickupScale;
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



    public void SpawnItem(Item.Type itemType, Item.EquipmentCategory equipType)
    {
        Item item = ItemsDatabaseBahaviour.Instance.GetAnItem(equipType, itemType);

        CreatePickupableItem(item, UnityEngine.Random.Range(2, PICKUPABLE_ITEM_GENERATION_RADIUS));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    #region SingletonPattern
    private static InventoryController _instance;
    public static InventoryController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryController>();
            }
            return _instance;
        }
    }

    #endregion
    public InventoryUI InventoryUI;
    private EquipmentController EquipmentController;
    private PlayerPickUpHandler PlayerPickUp;
    private const string GRID_PREFAB_KEY = "GridItem";
    private const string STACKABLE_GRID_PREFAB_KEY = "StackableGridItem";
    public KeyCode ITEM_DROP_KEY_SHORTCUT = KeyCode.R;

    private Dictionary<int, List<GridItem>> _items;
    public PlayerAttributes PlayerAttributes;

   
    public void ConsumeItem(List<ItemAttribute> attributes)
    {
        PlayerAttributes.EnableAttribute(attributes);
        Debug.Log("Item Cosumed");
    }
    public void ConsumeGridItem(GridItem item)
    {
        if (item is StackableGridItem)
        {
            StackableGridItem stkRef = item as StackableGridItem;
            int currentNumber = item.GetCurrentNumber();
            currentNumber -= 1;
            
            stkRef.SetCurrentNubmber(currentNumber);
            
            ConsumeItem(item.GetItemReference().Attributes);
            if (currentNumber==0)
             RemoveItemFromInventory(item,true);


        }
        else
        {
            ConsumeItem(item.GetItemReference().Attributes);
            RemoveItemFromInventory(item,true);
        }
    }

    void Start()
    {
        EquipmentController = GetComponent<EquipmentController>();
        PlayerPickUp = GetComponent<PlayerPickUpHandler>();

        _items = new Dictionary<int, List<GridItem>>();
    }

    public void AddToInventory(Item item, bool AutoEquip, bool ForceEquip = false)
    {

        if (AutoEquip)
        {
            if (item.Equipment != Item.EquipmentCategory.NotEquippable)
            {
                GridItem lastItem;
                if (HandleDirectEquip(item, ForceEquip, out lastItem))
                {
                    if (lastItem != null)
                    {
                        AddToInventory(lastItem.GetItemReference(), false, false);
                        ObjectPoolManager.Instance.RecycleObject(lastItem.GetComponent<PoolableObjectInstance>());
                    }
                    return;

                }
                else
                {
                }

            }
        }
        if (item.Stacking == Item.Stackability.Stackable)
        {
            if (_items.ContainsKey(item.Id))
            {
                List<GridItem> stackableItems = _items[item.Id];

                foreach (GridItem gItem in stackableItems)
                {
                    StackableGridItem sItem = gItem as StackableGridItem;
                    if (!sItem.IsItemAtTheMax())
                    {
                        int currentNumber = sItem.GetItemCount();
                        currentNumber++;
                        sItem.SetCurrentNubmber(currentNumber);
                        return;
                    }
                }
            }
            AddNewGridItem(item, true);
        }
        else
        {
            AddNewGridItem(item, false);
        }
    }
    public void RemoveItemFromInventory(GridItem item, bool destroyObject)
    {
        if (_items.ContainsKey(item.GetItemReference().Id))
        {
            List<GridItem> stackableItems = _items[item.GetItemReference().Id];
            stackableItems.Remove(item);
            if (stackableItems.Count == 0)
            {
                _items.Remove(item.GetItemReference().Id);
            }
            if (destroyObject)
            ObjectPoolManager.Instance.RecycleObject(item.GetComponent<PoolableObjectInstance>());

        }
    }
    public void ForceEquipItem(GridItem item)
    {
        RemoveItemFromInventory(item,true);
        AddToInventory(item.GetItemReference(), true, true);
    }
    public void SendItemToTheGround(GridItem _gridItem)
    {
        Item item = _gridItem.GetItemReference();
        for (int i = 0; i < _gridItem.GetItemCount(); i++)
        {
            PlayerPickUp.CreatePickupableItem(item, 2);
        }
    }
    bool HandleDirectEquip(Item item, bool forceEquip, out GridItem lastItem)
    {
        EquipmentController.Equipment equipment;
        bool result = EquipmentController.AddItemToSlot(item, forceEquip, out equipment);
        lastItem = equipment.EquipedItem;
        return result;

    }
    void AddNewGridItem(Item item, bool stackable)
    {
        GridItem gItem;

        if (!stackable)
        {
            gItem = ObjectPoolManager.Instance.GetObject<GridItem>(GRID_PREFAB_KEY);
        }
        else
        {
            gItem = ObjectPoolManager.Instance.GetObject<StackableGridItem>(STACKABLE_GRID_PREFAB_KEY);
        }

        InventoryUI.AddNewGridItem(gItem, item);

        if (_items.ContainsKey(item.Id))
        {
            List<GridItem> gridItems = new List<GridItem>();

            gridItems = _items[item.Id];

            gridItems.Add(gItem);
        }
        else
        {
            _items.Add(item.Id, new List<GridItem>() { gItem });
        }

    }
   


}

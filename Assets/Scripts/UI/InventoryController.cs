using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    //private List<Item> _items;
    // private Dictionary<int, List<GridItem>> _items;
    public InventoryUI InventoryUI;


    private const string GRID_PREFAB_KEY = "GridItem";
    private const string STACKABLE_GRID_PREFAB_KEY = "StackableGridItem";

    private Dictionary<int, List<GridItem>> _items;
    void Start()
    {
        _items = new Dictionary<int, List<GridItem>>();
    }

    public void AddToInventory(Item item)
    {
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
        gItem.Setup(item);
        InventoryUI.AddNewGridItem(gItem);

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

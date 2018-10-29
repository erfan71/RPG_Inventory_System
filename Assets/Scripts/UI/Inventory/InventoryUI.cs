using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : GeneralPanel
{
    public Transform GridRoot;
    private const string GRID_PREFAB_KEY = "GridItem";
    public void AddItemToGrid(Item item)
    {
        GridItem gItem= ObjectPoolManager.Instance.GetObject<GridItem>(GRID_PREFAB_KEY);
        gItem.Setup(item);
        Vector3 beforeScale = gItem.transform.localScale;
        gItem.transform.SetParent(GridRoot);
        gItem.transform.localScale = beforeScale;
    }
   
}

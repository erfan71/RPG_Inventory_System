using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnderTheMouse : MonoBehaviour
{

    private GridItem _gridItem;
    public InventoryUI InventoryUI;
    #region SingletonPattern
    private static ItemUnderTheMouse _instance;
    public static ItemUnderTheMouse Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ItemUnderTheMouse>();
            }
            return _instance;
        }
    }

    #endregion

    public void SetCurrentDragedItem(GridItem item)
    {
        _gridItem = item;
        InventoryUI.SetEventPanelActive(true);
    }

    public GridItem GetCurrentDragedItem()
    {
        return _gridItem;

    }
    public void ReleaseCurrentDraggedItem()
    {
        _gridItem = null;
        InventoryUI.SetEventPanelActive(false);

    }

}

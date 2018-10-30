using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnderTheMouse : MonoBehaviour
{

    private GridItem _gridItem;
    public InventoryUI InventoryUI;
    public EquipmentUI EquipmentUI;
    public InventoryController Inventory;
    public EquipmentController Equipment;

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

    private void Start()
    {
        InventoryUI.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
        EquipmentUI.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
    }
    private void OnDestroy()
    {
        InventoryUI.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
        EquipmentUI.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
    }
    public void SetCurrentDragedItem(GridItem item)
    {
        _gridItem = item;
        InventoryUI.SetEventPanelActive(true);
        if (item.GetContainedPanel() is InventoryUI)
            Inventory.RemoveItemFromInventory(_gridItem);
        else
            Equipment.RemoveItem(item.GetItemReference());
    }

    public GridItem GetCurrentDragedItem()
    {
        return _gridItem;

    }
    public void ReleaseCurrentDraggedItem()
    {
        InventoryUI.SetEventPanelActive(false);
        ObjectPoolManager.Instance.RecycleObject(_gridItem.GetComponent<PoolableObjectInstance>());
        _gridItem = null;


    }
    private void OnPanelOpenCloseActionCallBack(GeneralPanel panel)
    {
        //
        if (InventoryUI.State == GeneralPanel.PopUpState.Closed && EquipmentUI.State == GeneralPanel.PopUpState.Closed)
        {
            if (_gridItem != null)
            {
                if (_gridItem.GetContainedPanel() is EquipmentUI)
                    AddCurrentItemToInventory(true);
                else
                    AddCurrentItemToInventory(false);
            }
        }
        
    }
    public void OnBehindTheSceneClicked()
    {
        if
            ((InventoryUI.State == GeneralPanel.PopUpState.Closed && EquipmentUI.State == GeneralPanel.PopUpState.Opened) ||
             (InventoryUI.State == GeneralPanel.PopUpState.Opened && EquipmentUI.State == GeneralPanel.PopUpState.Closed))
        {
            if (_gridItem != null)
            {
                SendItemToTheGround();
            }
        }
    }
    public void AddCurrentItemToInventory(bool Equip)
    {
        if (_gridItem != null)
        {
            Item item = _gridItem.GetItemReference();
            for (int i = 0; i < _gridItem.GetItemCount(); i++)
            {
                Inventory.AddToInventory(item, Equip,true);
            }
            ReleaseCurrentDraggedItem();
        }
    }
    public void SendItemToTheGround()
    {
        if (_gridItem != null)
        {
            Item item = _gridItem.GetItemReference();
            for (int i = 0; i < _gridItem.GetItemCount(); i++)
            {
                Inventory.SendItemToTheGround(item);
            }
            ReleaseCurrentDraggedItem();
        }
    }
    public bool IsAnythingClicked()
    {
        if (_gridItem != null)
        {
            return true;
        }
        else
            return false;
            
                }
}

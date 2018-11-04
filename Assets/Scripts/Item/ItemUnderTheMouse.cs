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

    private bool _longPressMode = false;

    private const string UNDER_MOUSE_ICON_PREFAB_KEY = "UnderMouse";
    UnderMouseItem underItem;
    private bool _fromInventory = false;
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
    public void SetCurrentDragedItem(GridItem item, bool longPressMode)
    {
        _gridItem = item;
        InventoryUI.SetEventPanelActive(true);
        if (item.GetContainedPanel() is InventoryUI)
        {
            _fromInventory = true;
            Inventory.RemoveItemFromInventory(_gridItem, false);
        }
        else
        {
            Equipment.RemoveItem(item.GetItemReference());
            _fromInventory = false;
        }

        underItem = ObjectPoolManager.Instance.GetObject<UnderMouseItem>(UNDER_MOUSE_ICON_PREFAB_KEY);
        underItem.Setup(item.GetItemReference().Image);
        underItem.transform.SetParent(InventoryUI.CanvasRoot.transform);
        underItem.transform.localScale = Vector2.one;
        ObjectPoolManager.Instance.RecycleObject(_gridItem.GetComponent<PoolableObjectInstance>());
        StartCoroutine(InTheAirControl());
        _longPressMode = longPressMode;


    }
    private void SetPosition(Vector2 screenPos)
    {
        underItem.transform.position = screenPos;
    }


    private IEnumerator InTheAirControl()
    {
        while (_gridItem != null)
        {
            if (Input.GetKeyDown(InventoryController.Instance.ITEM_DROP_KEY_SHORTCUT))
            {
                SendItemToTheGround();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (_longPressMode)
                {
                    if (_lastEnterEquipmentSlot != null)
                    {
                        EquipLogic();
                    }
                    else
                    {
                        if (InventoryDropped)
                        {
                            AddCurrentItemToInventory(false);

                        }
                        else
                        {
                            if (!OnBehindTheSceneLogic())
                            {
                                AddCurrentItemToInventory(!_fromInventory);

                            }              
                        }
                    }
                }
            }
            else
                SetPosition(Input.mousePosition);
            yield return null;
        }
    }
    private EquipmentSlot _lastEnterEquipmentSlot;
    private EquipmentSlot _startDraggedEquipmentSlot;
    private bool InventoryDropped = false;

    public void EnterEquipmentSlot(EquipmentSlot equipmentSlot)
    {
        _lastEnterEquipmentSlot = equipmentSlot;
    }
    public void EnterInventory()
    {
        InventoryDropped = true;
    }
    public void ExitInventory()
    {
        InventoryDropped = false;

    }
    public void ExitEquipmentSlot()
    {
        _lastEnterEquipmentSlot = null;
    }

    void EquipLogic()
    {
        if (_gridItem.GetItemReference().Equipment == _lastEnterEquipmentSlot.EquipmentType)
        {
            AddCurrentItemToInventory(true);
        }
        else
        {
            if (_gridItem.GetItemReference().Equipment == Item.EquipmentCategory.NotEquippable)
            {
                Debug.LogError("Not Equipable");
                AddCurrentItemToInventory(!_fromInventory);
                FloatingTexts.Instance.Show("Not Equipable", FloatingTexts.Type.Error);
            }
            else
            {
                Debug.LogError("Not Suitable for this slot: " + _lastEnterEquipmentSlot.EquipmentType.ToString());
                AddCurrentItemToInventory(!_fromInventory);
                FloatingTexts.Instance.Show("Not Suitable for this slot: " + _lastEnterEquipmentSlot.EquipmentType.ToString(), FloatingTexts.Type.Error);
            }
        }
    }

    public GridItem GetCurrentDragedItem()
    {
        return _gridItem;
    }
    public void ReleaseCurrentDraggedItem()
    {
        InventoryUI.SetEventPanelActive(false);
        ObjectPoolManager.Instance.RecycleObject(underItem.GetComponent<PoolableObjectInstance>());
        _gridItem = null;
        _longPressMode = false;
        Debug.Log("ReleaseCurrentDraggedItem");
    }
    private void OnPanelOpenCloseActionCallBack(GeneralPanel panel)
    {
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
    public bool OnBehindTheSceneLogic()
    {
        if
            ((InventoryUI.State == GeneralPanel.PopUpState.Closed && EquipmentUI.State == GeneralPanel.PopUpState.Opened) ||
             (InventoryUI.State == GeneralPanel.PopUpState.Opened && EquipmentUI.State == GeneralPanel.PopUpState.Closed))
        {
            if (_gridItem != null)
            {
                SendItemToTheGround();
                return true;
            }
            return false;
        }
        return false;
    }
    public void  OnBehindTheSceneClicked()
    {
        OnBehindTheSceneLogic();
    }
    public void AddCurrentItemToInventory(bool Equip)
    {
        if (_gridItem != null)
        {
            Item item = _gridItem.GetItemReference();
            for (int i = 0; i < _gridItem.GetItemCount(); i++)
            {
                Inventory.AddToInventory(item, Equip, true);
            }
            ReleaseCurrentDraggedItem();
        }
    }
    public void SendItemToTheGround()
    {
        if (_gridItem != null)
        {
            Inventory.SendItemToTheGround(_gridItem);
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

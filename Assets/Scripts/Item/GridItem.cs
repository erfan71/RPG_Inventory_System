using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GridItem : ItemBehaviour, IPointerClickHandler
{

    public Image GridIcon;
    private GeneralPanel parentPanel;
    protected int _currentNumber;


    private enum State
    {
        InTheAir,
        Grounded
    }
    private State _state;
    public override void Setup(Item item)
    {
        base.Setup(item);
        SetImage();
        _state = State.Grounded;
        parentPanel = GetComponentInParent<GeneralPanel>();
        GetComponent<Image>().raycastTarget = true;
        _currentNumber = 1;
    }
    public int GetItemCount()
    {
        return _currentNumber;
    }
    private void SetImage()
    {
        GridIcon.sprite = _item.Image;
    }

    private void GoIntoTheAir(bool longPress)
    {
        _state = State.InTheAir;
        transform.SetParent(parentPanel.CanvasRoot.transform);
        //StartCoroutine(InTheAirControl());
        GetComponent<Image>().raycastTarget = false;
        ItemUnderTheMouse.Instance.SetCurrentDragedItem(this, longPress);
    }
    float _lastClick = 0f;
    float _interval = 0.4f;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        if (ItemUnderTheMouse.Instance.IsAnythingClicked())
        {
            ItemUnderTheMouse.Instance.AddCurrentItemToInventory(true);
        }
        else
        {

#if (UNITY_ANDROID && !UNITY_EDITOR) || INPUT_DEBUG
           
            if ((_lastClick + _interval) > Time.time)
            {
                if (_item.Equipment != Item.EquipmentCategory.NotEquippable)
                {
                    if (EquipmentController.Instance.IsItemEquiped(this))
                    {
                        EquipmentController.Instance.RemoveItem(this.GetItemReference());
                        InventoryController.Instance.AddToInventory(this.GetItemReference(), false);
                        ObjectPoolManager.Instance.RecycleObject(this.GetComponent<PoolableObjectInstance>());
                    }
                    else
                    {
                        InventoryController.Instance.ForceEquipItem(this);
                    }
                }             
                else if (_item.Consuming == Item.Consumability.Consumable)
                {
                    InventoryController.Instance.ConsumeGridItem(this);
                }
               
            }
            _lastClick = Time.time;
#else

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (_state == State.Grounded)
                    GoIntoTheAir(false);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_item.Equipment != Item.EquipmentCategory.NotEquippable)
                {
                    if (EquipmentController.Instance.IsItemEquiped(this))
                    {
                        EquipmentController.Instance.RemoveItem(this.GetItemReference());
                        InventoryController.Instance.AddToInventory(this.GetItemReference(), false);
                        ObjectPoolManager.Instance.RecycleObject(this.GetComponent<PoolableObjectInstance>());
                    }
                    else
                    {
                        InventoryController.Instance.ForceEquipItem(this);
                    }
                }
                else
                {
                    Debug.LogError("Not Equipable");
                    FloatingTexts.Instance.Show("Not Equipable", FloatingTexts.Type.Error);

                }
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                if (_item.Consuming == Item.Consumability.Consumable)
                {
                    InventoryController.Instance.ConsumeGridItem(this);
                }
                else
                {
                    Debug.LogError("Not Consumable");
                    FloatingTexts.Instance.Show("Not Consumable",FloatingTexts.Type.Error);
                }
            }
#endif
        }
    }


    public GeneralPanel GetContainedPanel()
    {
        return parentPanel;
    }
    public int GetCurrentNumber()
    {
        return _currentNumber;
    }

    public void OnLongPress()
    {
        if (_state == State.Grounded)
            GoIntoTheAir(true);
    }


}

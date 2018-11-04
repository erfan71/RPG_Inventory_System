using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{

    private GridItem _gridItem;
    public Item.EquipmentCategory EquipmentType;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SelectLogic();
    }
    void SelectLogic()
    {
        GridItem gridItem = ItemUnderTheMouse.Instance.GetCurrentDragedItem();

        if (gridItem != null)
        {
            if (gridItem.GetItemReference().Equipment == EquipmentType)
            {
                ItemUnderTheMouse.Instance.AddCurrentItemToInventory(true);
            }
            else
            {
                if (gridItem.GetItemReference().Equipment == Item.EquipmentCategory.NotEquippable)
                {
                    Debug.LogError("Not Equipable");
                    FloatingTexts.Instance.Show("Not Equipable", FloatingTexts.Type.Error);

                }
                else
                {
                    Debug.LogError("Not Suitable for this slot: " + EquipmentType.ToString());
                    FloatingTexts.Instance.Show("Not Suitable for this slot: " + EquipmentType.ToString(), FloatingTexts.Type.Error);

                }
            }
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ItemUnderTheMouse.Instance.EnterEquipmentSlot(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ItemUnderTheMouse.Instance.ExitEquipmentSlot();

    }
}

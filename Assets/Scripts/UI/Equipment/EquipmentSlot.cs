using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{

    private GridItem _gridItem;
    public Item.EquipmentCategory EquipmentType;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GridItem gridItem = ItemUnderTheMouse.Instance.GetCurrentDragedItem();

        if (gridItem.GetItemReference().Equipment == EquipmentType)
        {
          //  EquipmentUI.Instance.AddNewGridItem(gridItem, gridItem.GetItemReference());
           // ItemUnderTheMouse.Instance.ReleaseCurrentDraggedItem();
            ItemUnderTheMouse.Instance.AddCurrentItemToInventory(true);
        }
        else
        {
            if (gridItem.GetItemReference().Equipment == Item.EquipmentCategory.NotEquippable)
            {
                Debug.LogError("Not Equipable");
            }
            else
            {
                Debug.LogError("Not Suitable for this slot: "+ EquipmentType.ToString());

            }
        }
    }
}

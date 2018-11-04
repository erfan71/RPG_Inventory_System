using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelEventController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler {

    public InventoryUI InventoryUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryUI.OnPanelClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemUnderTheMouse.Instance.EnterInventory();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemUnderTheMouse.Instance.ExitInventory();
    }

}

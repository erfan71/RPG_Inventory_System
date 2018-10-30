using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : GeneralPanel
{
    public Transform GridRoot;
    public Image PanelEventDetector;
    private void Awake()
    {
    }
    public override void AddNewGridItem(GridItem gItem, Item item)
    {  
        gItem.transform.SetParent(GridRoot);
        gItem.transform.localScale = Vector3.one;
        gItem.Setup(item);
    }
    public void SetEventPanelActive(bool enable)
    {
        PanelEventDetector.raycastTarget = enable;
    }
    public void OnPanelClicked()
    { 
        ItemUnderTheMouse.Instance.AddCurrentItemToInventory(false);
    }
    

}

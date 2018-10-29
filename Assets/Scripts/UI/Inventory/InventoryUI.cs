using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : GeneralPanel
{
    public Transform GridRoot;

   

    private void Awake()
    {
    }
    public void AddNewGridItem(GridItem gItem)
    {  
        Vector3 beforeScale = gItem.transform.localScale;
        gItem.transform.SetParent(GridRoot);
        gItem.transform.localScale = beforeScale;      
    }
   
}

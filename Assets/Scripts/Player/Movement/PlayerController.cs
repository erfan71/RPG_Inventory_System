using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GeneralPanel InventroyPanel;
    public GeneralPanel EquipmentPanel;
	void Start () {
        InventroyPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
        EquipmentPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;

    }

    private void OnPanelOpenCloseActionCallBack(GeneralPanel panel)
    {
        if (panel.State == GeneralPanel.PopUpState.Opened)
        {
            if (panel is InventoryUI)
            {
                EquipmentPanel.ClosePanel();
            }
            else if (panel is EquipmentUI)
            {
                InventroyPanel.ClosePanel();
            }
        }
    }

    
}

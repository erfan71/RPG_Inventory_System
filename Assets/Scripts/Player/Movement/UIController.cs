using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GeneralPanel InventroyPanel;
    public GeneralPanel EquipmentPanel;
    public GameObject SpawningPanel;
	void Start () {
        InventroyPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
        EquipmentPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;

    }
    private void OnDestroy()
    {
        InventroyPanel.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
        EquipmentPanel.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
    }

    private void OnPanelOpenCloseActionCallBack(GeneralPanel panel)
    {
       if (InventroyPanel.State==GeneralPanel.PopUpState.Opened || EquipmentPanel.State == GeneralPanel.PopUpState.Opened)
        {
            SpawningPanel.SetActive(false);
        }
       else if (InventroyPanel.State == GeneralPanel.PopUpState.Closed && EquipmentPanel.State == GeneralPanel.PopUpState.Closed)
        {
            SpawningPanel.SetActive(true);

        }
    }

    
}

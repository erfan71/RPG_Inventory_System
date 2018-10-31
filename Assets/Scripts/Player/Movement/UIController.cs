using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GeneralPanel InventroyPanel;
    public GeneralPanel EquipmentPanel;
    public GeneralPanel SpawningPanel;
    public GeneralPanel AttributePanel;
    void Start()
    {
        InventroyPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
        EquipmentPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
        SpawningPanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;
        AttributePanel.OnOpenCloseActionCallBack += OnPanelOpenCloseActionCallBack;

    }
    private void OnDestroy()
    {
        InventroyPanel.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
        EquipmentPanel.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
        SpawningPanel.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;
        AttributePanel.OnOpenCloseActionCallBack -= OnPanelOpenCloseActionCallBack;

    }

    private void OnPanelOpenCloseActionCallBack(GeneralPanel panel)
    {
        if (InventroyPanel.State == GeneralPanel.PopUpState.Opened || EquipmentPanel.State == GeneralPanel.PopUpState.Opened)
        {
            if (SpawningPanel.State == GeneralPanel.PopUpState.Opened)
                SpawningPanel.ClosePanel();
            if (AttributePanel.State == GeneralPanel.PopUpState.Opened)
                AttributePanel.ClosePanel();

            SpawningPanel.SetOpenButtonActive(false);
            AttributePanel.SetOpenButtonActive(false);

        }
        else if (InventroyPanel.State == GeneralPanel.PopUpState.Closed && EquipmentPanel.State == GeneralPanel.PopUpState.Closed)
        {
            if (SpawningPanel.State == GeneralPanel.PopUpState.Closed)
                SpawningPanel.SetOpenButtonActive(true);
            if (AttributePanel.State == GeneralPanel.PopUpState.Closed)
                AttributePanel.SetOpenButtonActive(true);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : GeneralPanel {

    private Transform _helmetRoot;
    private Transform _primaryWeaponRoot;
    private Transform _secondaryWeaponRoot;
    private Transform _shoesRoot;
    private Transform _ringRoot;
    private Transform _medalRoot;
    private Transform _amuletRoot;

    private const string HELMET_CHILD_NAME = "Helmet";
    private const string PRIMARY_CHILD_NAME = "Primary";
    private const string SECONDARY_CHILD_NAME = "Secondary";
    private const string SHOES_CHILD_NAME = "Shoes";
    private const string RING_CHILD_NAME = "Ring";
    private const string AMULET_CHILD_NAME = "Amulet";
    private const string MEDAL_CHILD_NAME = "Medal";

    public RectTransform ChildsRoot;
    private void Awake()
    {
        _helmetRoot = ChildsRoot.Find(HELMET_CHILD_NAME);
        _primaryWeaponRoot = ChildsRoot.Find(PRIMARY_CHILD_NAME);
        _secondaryWeaponRoot = ChildsRoot.Find(SECONDARY_CHILD_NAME);
        _shoesRoot = ChildsRoot.Find(SHOES_CHILD_NAME);
        _ringRoot = ChildsRoot.Find(RING_CHILD_NAME);
        _amuletRoot = ChildsRoot.Find(AMULET_CHILD_NAME);
        _medalRoot = ChildsRoot.Find(MEDAL_CHILD_NAME);

    }
    public void AddNewGridItem(GridItem gItem)
    {
        Vector3 beforeScale = gItem.transform.localScale;
        Transform selectedParent = null;
        switch (gItem.GetItemReference().Equipment)
        {
            case Item.EquipmentCategory.PrimaryWeapon:
                selectedParent = _primaryWeaponRoot;
                break;
            case Item.EquipmentCategory.SecondaryWeapon:
                selectedParent = _secondaryWeaponRoot;

                break;
            case Item.EquipmentCategory.Helmet:
                selectedParent = _helmetRoot;

                break;
            case Item.EquipmentCategory.Shoes:
                selectedParent = _shoesRoot;

                break;
            case Item.EquipmentCategory.Medal:
                selectedParent = _medalRoot;

                break;
            case Item.EquipmentCategory.Amulet:
                selectedParent = _amuletRoot;

                break;
            case Item.EquipmentCategory.Ring:
                selectedParent = _ringRoot;

                break;
        }
        gItem.transform.SetParent(selectedParent);
        gItem.transform.localScale = beforeScale;

        RectTransform rect = gItem.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax= new Vector2(0.5f, 0.5f);
        rect.pivot= new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = new Vector2(0, 0);
        rect.sizeDelta = selectedParent.GetComponent<RectTransform>().sizeDelta;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : GeneralPanel {


    private const string ATTRIBUTE_UI_ITEM_PREFAB_KEY = "AttributeUIItem";
    public Transform AttributeUIItemsParent;

    private Dictionary<Attribute, AttributeUIItem> AttributeUIItems;
    public void Setup()
    {
        AttributeUIItems = new Dictionary<Attribute, AttributeUIItem>();
    }

    public void AddAttributeUIItem (Attribute attr)
    {
        AttributeUIItem attributeUIItem = ObjectPoolManager.Instance.GetObject<AttributeUIItem>(ATTRIBUTE_UI_ITEM_PREFAB_KEY);
        attributeUIItem.transform.SetParent(AttributeUIItemsParent);
        attributeUIItem.transform.localScale = Vector2.one;
        attributeUIItem.Setup();
        attributeUIItem.SetAttribute(attr);
        AttributeUIItems.Add(attr, attributeUIItem);
    }


}

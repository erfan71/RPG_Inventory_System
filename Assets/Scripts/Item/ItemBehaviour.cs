using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

    private int _itemId;
    protected Item _item;
    public virtual void Setup(int itemId)
    {
        _itemId = itemId;
        Item item = ItemsDatabaseBahaviour.Instance.GetItem(_itemId);
        this._item = item;
    }
    public virtual void Setup(Item item)
    {
        this._item = item;
    }

    public Item GetItemReference()
    {
        return _item;
    }
    public string GetItemName()
    {
        return _item.Name;
    }
}

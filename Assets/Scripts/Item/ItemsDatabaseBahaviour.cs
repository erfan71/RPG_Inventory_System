using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabaseBahaviour : MonoBehaviour
{
    #region SingletonPattern
    private static ItemsDatabaseBahaviour _instance;
    public static ItemsDatabaseBahaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ItemsDatabaseBahaviour>();
            }
            return _instance;
        }
    }

    #endregion
    private Dictionary<int, Item> _itemsDictionary;
    private Dictionary<Item.Type, int> _itemTypesMaxDic;
    private bool isSetuped;
    private ItemsDatabase _itemsdDatabase;

    private Dictionary<Item.Type, List<Item>> _equipmentItems;

    private void Awake()
    {
        isSetuped = false;
        _itemsdDatabase = Resources.Load<ItemsDatabase>("ItemsDatabase");
        SetupCache();
    }
    void SetupCache()
    {
        if (!isSetuped)
        {
            _itemsDictionary = new Dictionary<int, Item>();
            _equipmentItems = new Dictionary<Item.Type, List<Item>>();
            foreach (Item item in _itemsdDatabase.Items)
            {
                _itemsDictionary.Add(item.Id, item);

               
                    if (_equipmentItems.ContainsKey(item.ItemType))
                    {
                       List<Item> currentITems=  _equipmentItems[item.ItemType];
                        currentITems.Add(item);
                        _equipmentItems[item.ItemType] = currentITems;
                    }
                    else
                    {
                        _equipmentItems.Add(item.ItemType, new List<Item>() { item });
                    }
                
            }
            _itemTypesMaxDic = new Dictionary<Item.Type, int>();
            foreach (ItemsDatabase.TypeMaxStack item in _itemsdDatabase.TypesStackMax)
            {
                if (item.Limitation==Item.MaxType.Limited)
                    _itemTypesMaxDic.Add(item.Type, item.Max);
            }
            

            isSetuped = true;
        }

    }
    public Item GetItem(int itemId)
    {
        CheckCache();
        if (_itemsDictionary.ContainsKey(itemId))
            return _itemsDictionary[itemId];
        else
            return null;
    }
    void CheckCache()
    {
        if (!isSetuped)
        {
            SetupCache();
        }
    }
    public Sprite GetItemSprite(int itemId)
    {
      return  GetItem(itemId).Image;
    }
    public int GetItemTypeMaxStackCount(Item.Type type)
    {
        if (_itemTypesMaxDic.ContainsKey(type))
        {
            return _itemTypesMaxDic[type];
        }
        else
            return -1;
    }

    public Item GetAnItem(Item.EquipmentCategory equipType, Item.Type itemType)
    {
        //It is not a very optimized solution. we can easily cash "selectedItems" too. But currently, we suppose there are not many Item.
        System.Random random = new System.Random();
      
        List<Item> itemsOFType = _equipmentItems[itemType];
        List<Item> selectedItems = new List<Item>();
        foreach( Item item in itemsOFType)
        {
            if (item.Equipment== equipType)
            {
                selectedItems.Add(item);
            }
        }
        Item selectedItem= selectedItems[random.Next(selectedItems.Count)];

        return selectedItem;
    }
   
}

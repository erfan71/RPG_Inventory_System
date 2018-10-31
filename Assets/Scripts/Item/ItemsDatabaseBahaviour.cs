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

    private Dictionary<Item.EquipmentCategory, List<Item>> _equipmentItems;

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
            _equipmentItems = new Dictionary<Item.EquipmentCategory, List<Item>>();
            foreach (Item item in _itemsdDatabase.Items)
            {
                _itemsDictionary.Add(item.Id, item);

                if (item.Equipment != Item.EquipmentCategory.NotEquippable)
                {
                    if (_equipmentItems.ContainsKey(item.Equipment))
                    {
                       List<Item> currentITems=  _equipmentItems[item.Equipment];
                        currentITems.Add(item);
                        _equipmentItems[item.Equipment] = currentITems;
                    }
                    else
                    {
                        _equipmentItems.Add(item.Equipment, new List<Item>() { item });
                    }
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

    public Item GetAnEquipmentItem(Item.EquipmentCategory equipType)
    {
        System.Random random = new System.Random();
      
        List<Item> itemsOFType = _equipmentItems[equipType];
        Item selectedItem= itemsOFType[random.Next(itemsOFType.Count)];

        return selectedItem;

    }
}

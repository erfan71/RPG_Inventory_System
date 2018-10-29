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
            foreach (Item item in _itemsdDatabase.Items)
            {
                _itemsDictionary.Add(item.Id, item);
            }
            _itemTypesMaxDic = new Dictionary<Item.Type, int>();
            foreach (ItemsDatabase.TypeMaxStack item in _itemsdDatabase.TypesStackMax)
            {
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
       return  _itemTypesMaxDic[type];
    }
}

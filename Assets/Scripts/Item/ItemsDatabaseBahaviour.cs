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
    private Dictionary<int, Item> _itemDictionary;
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
            _itemDictionary = new Dictionary<int, Item>();
            foreach (Item item in _itemsdDatabase.Items)
            {
                _itemDictionary.Add(item.Id, item);
            }
            isSetuped = true;
        }

    }
    public Item GetItem(int itemId)
    {
        CheckCache();
        if (_itemDictionary.ContainsKey(itemId))
            return _itemDictionary[itemId];
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
}

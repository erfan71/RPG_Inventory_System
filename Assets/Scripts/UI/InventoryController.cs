using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    private List<Item> _items;
    public InventoryUI InventoryUI;
	void Start () {
        _items = new List<Item>();

    }
	
	public void AddToInventory(Item item)
    {
        _items.Add(item);
        InventoryUI.AddItemToGrid(item);
    }
   
   
}

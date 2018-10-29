using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Items Database", order = 1)]
public class ItemsDatabase : ScriptableObject {

    public List<Item> Items;
    
    [System.Serializable]
    public struct TypeMaxStack
    {
        public Item.Type Type;
        public int Max;
    }
    public List<TypeMaxStack> TypesStackMax;
}

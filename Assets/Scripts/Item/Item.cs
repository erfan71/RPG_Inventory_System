using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    public enum EquipmentCategory
    {
        PrimaryWeapon,
        SecondaryWeapon,
        Helmet,
        Torse,
        NotEquippable
    }
    public enum Stackability
    {
        Stackable,
        NotStackable
    }
    public enum Consumability
    {
        Consumable,
        NotConsumable
    }
    public enum Type
    {
        Coin,
        Food,
        Weapon,
        Poition
    }

    public int Id;
    public Sprite Image;
    public EquipmentCategory Equipment;
    public Stackability Stacking;
    public Consumability Consuming;
    public Type ItemType;
    public string Name;

}

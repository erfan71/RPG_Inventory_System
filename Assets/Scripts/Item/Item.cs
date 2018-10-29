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
        Equippable
    }
    public enum Stackability
    {
        Stackable,
        NotStackable
    }
    public enum Consumability
    {
        Consume,
        NotConsumable
    }

    public int Id;
    public Sprite Image;
    public EquipmentCategory Equipment;
    public Stackability Stacking;
    public Consumability Consuming;
    public string Name;

}

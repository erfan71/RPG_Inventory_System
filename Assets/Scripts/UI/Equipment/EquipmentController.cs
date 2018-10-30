using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour {

    public EquipmentUI EquipmentUI;
    public enum SlotState
    {
        Equiped,
        NotEquiped,
    }
    public struct Equipment
    {
        public SlotState EquipmentState;
        public Item EquipedItem;
        public Equipment(SlotState equipmentState , Item equipedItem)
        {
            EquipmentState = equipmentState;
            EquipedItem = equipedItem;

        }
    }

    public Dictionary<Item.EquipmentCategory, Equipment> Equipments;

    private const string GRID_PREFAB_KEY = "GridItem";

    private void Start()
    {
        Equipments = new Dictionary<Item.EquipmentCategory, Equipment>();
           var EquipmentCategories = System.Enum.GetValues(typeof(Item.EquipmentCategory));

        foreach(Item.EquipmentCategory item in EquipmentCategories)
        {
            Equipments.Add(item, new Equipment(SlotState.NotEquiped, null));
        }
    }
    private SlotState GetSlotState(Item.EquipmentCategory item)
    {
       return Equipments[item].EquipmentState;
    }
    public bool AddItemToSlot(Item item)
    {
        if (GetSlotState(item.Equipment) == SlotState.NotEquiped)
        {
            EquipItem(item);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void EquipItem(Item item)
    {
        GridItem gItem= ObjectPoolManager.Instance.GetObject<GridItem>(GRID_PREFAB_KEY);
        gItem.Setup(item);
        EquipmentUI.AddNewGridItem(gItem);

        if (Equipments.ContainsKey(item.Equipment))
        {

        }
        else
        {

        }

        Equipments[item.Equipment]=new Equipment(SlotState.Equiped,item);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    #region SingletonPattern
    private static EquipmentController _instance;
    public static EquipmentController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EquipmentController>();
            }
            return _instance;
        }
    }

    #endregion

    public EquipmentUI EquipmentUI;
    public enum SlotState
    {
        Equiped,
        NotEquiped,
    }
    public struct Equipment
    {
        public SlotState EquipmentState;
        public GridItem EquipedItem;
        public Equipment(SlotState equipmentState, GridItem equipedItem)
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

        foreach (Item.EquipmentCategory item in EquipmentCategories)
        {
            Equipments.Add(item, new Equipment(SlotState.NotEquiped, null));
        }
    }
    private SlotState GetSlotState(Item.EquipmentCategory item)
    {
        return Equipments[item].EquipmentState;
    }
    public bool AddItemToSlot(Item item, bool replace, out Equipment lastEquipment)
    {
        lastEquipment = new Equipment(SlotState.NotEquiped, null);
        if (GetSlotState(item.Equipment) == SlotState.NotEquiped)
        {
            EquipItem(item);
            return true;
        }
        else
        {
            if (replace)
            {
                Equipment lastEq = UnEquipItem(item.Equipment);
                EquipItem(item);
                lastEquipment = lastEq;
                return true;
            }
            else
                return false;

        }
    }
    public void RemoveItem(Item item)
    {
        if (GetSlotState(item.Equipment) == SlotState.Equiped)
        {
            UnEquipItem(item);
        }
        else
        {
        }
    }
    private Equipment UnEquipItem(Item item)
    {
        Equipment lastEq = Equipments[item.Equipment];
        Equipments[item.Equipment] = new Equipment(SlotState.NotEquiped, null);
        return lastEq;
    }
    private Equipment UnEquipItem(Item.EquipmentCategory category)
    {
        Equipment lastEq = Equipments[category];
        Equipments[category] = new Equipment(SlotState.NotEquiped, null);
        return lastEq;

    }
    private Equipment EquipItem(Item item)
    {
        GridItem gItem = ObjectPoolManager.Instance.GetObject<GridItem>(GRID_PREFAB_KEY);
        EquipmentUI.AddNewGridItem(gItem, item);


        Equipments[item.Equipment] = new Equipment(SlotState.Equiped, gItem);
        return Equipments[item.Equipment];
    }
    public bool IsItemEquiped(GridItem item)
    {
        if (Equipments.ContainsKey(item.GetItemReference().Equipment))
        {
            if (item== Equipments[item.GetItemReference().Equipment].EquipedItem)
            {
                return true;
            }
        }
        return false;
    }

}

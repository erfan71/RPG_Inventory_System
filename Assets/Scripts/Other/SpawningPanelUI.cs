using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawningPanelUI : GeneralPanel {

    public PlayerPickUpHandler PickupableManager;


    public Button PrimaryButton;
    public Button SecondaryButton;
    public Button HelmetButton;
    public Button ShoesButton;
    public Button FoodButton;
    public Button CoinButton;
    public Button PoisonButton;

	void Awake () {

        PrimaryButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Weapon_Equiable,Item.EquipmentCategory.PrimaryWeapon));
        SecondaryButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Weapon_Equiable, Item.EquipmentCategory.SecondaryWeapon));
        HelmetButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Weapon_Equiable, Item.EquipmentCategory.Helmet));
        ShoesButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Weapon_Equiable, Item.EquipmentCategory.Shoes));
        FoodButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Food, Item.EquipmentCategory.NotEquippable));
        CoinButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Coin, Item.EquipmentCategory.NotEquippable));
        PoisonButton.onClick.AddListener(() => PickupableManager.SpawnItem(Item.Type.Poison, Item.EquipmentCategory.NotEquippable));


    }


}

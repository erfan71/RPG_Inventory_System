using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {

    public enum AttributeType
    {
        Strength,
        Dexterity,
        Agility,
        Intelligence
    }
    public Attribute Strength;
    public Attribute Dexterity;
    public Attribute Agility;
    public Attribute Intelligence;

    public AttributeUI _attributeUI;
    private void Start()
    {
        Strength = new Attribute(AttributeType.Strength, 10.565f, 100, "Strength");
        Dexterity = new Attribute(AttributeType.Dexterity, 10, 100, "Dexterity");
        Agility = new Attribute(AttributeType.Agility, 10, 100, "Agility");
        Intelligence = new Attribute(AttributeType.Intelligence, 10, 100, "Intelligence");

        _attributeUI.Setup();

        _attributeUI.AddAttributeUIItem(Strength);
        _attributeUI.AddAttributeUIItem(Dexterity);
        _attributeUI.AddAttributeUIItem(Agility);
        _attributeUI.AddAttributeUIItem(Intelligence);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {

    public enum AttributeType
    {
        Attack,
        Defence,
        Agility,
        Luck,
        Mana,
        Health
    }
    public Attribute Attack;
    public Attribute Defence;
    public Attribute Agility;
    public Attribute Luck;
    public Attribute Mana;
    public Attribute Health;

    public AttributeUI _attributeUI;
    private void Start()
    {
        Attack = new Attribute(AttributeType.Attack, 10.565f, 100,-100, AttributeType.Attack.ToString());
        Defence = new Attribute(AttributeType.Defence, 10, 100,-100, AttributeType.Defence.ToString());
        Agility = new Attribute(AttributeType.Agility, 10, 100,-100, AttributeType.Agility.ToString());
        Luck = new Attribute(AttributeType.Luck, 10, 100,-100, AttributeType.Luck.ToString());
        Mana = new Attribute(AttributeType.Mana, 50, 100,0, AttributeType.Mana.ToString());
        Health = new Attribute(AttributeType.Health, 50, 100,0, AttributeType.Health.ToString());

        _attributeUI.Setup();

        _attributeUI.AddAttributeUIItem(Health);
        _attributeUI.AddAttributeUIItem(Mana);
        _attributeUI.AddAttributeUIItem(Attack);
        _attributeUI.AddAttributeUIItem(Defence);
        _attributeUI.AddAttributeUIItem(Agility);
        _attributeUI.AddAttributeUIItem(Luck);
    }

    public void EnableAttribute(List<ItemAttribute> item)
    {
        foreach(ItemAttribute itemAtt in item)
        {
            Attribute attr = (Attribute)this.GetType().GetField(itemAtt.Type.ToString()).GetValue(this);
            attr.AddValue(itemAtt.EffectValue);
            _attributeUI.UpdateAttributeUIItemValue(attr);
        }
       
    }
    public void DisableAttribute(List<ItemAttribute> item)
    {
        foreach (ItemAttribute itemAtt in item)
        {
            Attribute attr = (Attribute)this.GetType().GetField(itemAtt.Type.ToString()).GetValue(this);
            attr.SubtractValue(itemAtt.EffectValue);
            _attributeUI.UpdateAttributeUIItemValue(attr);
        }

    }

}

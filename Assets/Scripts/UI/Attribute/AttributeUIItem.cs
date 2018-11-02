using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUIItem : MonoBehaviour {

    private Attribute _attribute;

    public Attribute Attribute
    {
        get
        {
            return _attribute;
        }
    }
    private Text _name;
    public Text Name
    {
        get
        {
            return _name;
        }
    }
    private Text _value;
    public Text Value
    {
        get
        {
            return _value;
        }
        
    }
    public void Setup()
    {
        _name = transform.GetChild(0).GetComponent<Text>();
        _value = transform.GetChild(1).GetComponent<Text>();

    }
    public void SetAttribute(Attribute attr)
    {
        _attribute = attr;
        _name.text = attr.Name;
        SetProperValue(attr.Value);
    }
    private void SetProperValue(float value)
    {
        _value.text = value.ToString("0.##");

    }
    public void SetValue(float value)
    {
        SetProperValue(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute {

	
    public PlayerAttributes.AttributeType Type;
    private float _value;
    public float Value
    {
        get
        {
            return _value;
        }
    }
    private int _maxValue;
    public int MaxValue
    {
        get
        {
            return _maxValue;
        }
    }
    private int _minValue;
    public int MinValue
    {
        get
        {
            return _minValue;
        }
    }
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }

    public Attribute(PlayerAttributes.AttributeType type, float defaultValue, int maxValue, int minValue, string name)
    {
        _value = defaultValue;
        _maxValue = maxValue;
        _name = name;
        Type = type;
        _minValue = minValue;
    }
    public void AddValue(float value)
    {
        _value = Mathf.Clamp(_value + value, _minValue, _maxValue);
    }


}

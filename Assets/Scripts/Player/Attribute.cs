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


    public Attribute(PlayerAttributes.AttributeType type, float defaultValue, int maxValue)
    {
        _value = defaultValue;
        _maxValue = maxValue;
    }


}

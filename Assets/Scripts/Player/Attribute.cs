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
    public void SubtractValue(float value)
    {
        _value = Mathf.Clamp(_value - value, _minValue, _maxValue);
    }

    public void AddValueForFixedDuration(float value, float duration)
    {
        CoroutineHandler.Instance.StartCoroutine(AddValueTemporarRoutine(value, duration));
    }
    public void AddValueOverTimeWithHolding(float value, float rampTime, float holdTime)
    {
        CoroutineHandler.Instance.StartCoroutine(AddValueOverTimeWithHoldingRoutine(value, rampTime, holdTime));
    }
    public void AddValuePerSecond(float valuePerSecond, float duration)
    {
        CoroutineHandler.Instance.StartCoroutine(AddValuePerSecondRoutine(valuePerSecond, duration));
    }
    IEnumerator AddValueTemporarRoutine(float value, float duration)
    {
        AddValue(value);
        AttributeUI.Instance.UpdateAttributeUIItemValue(this);
        yield return new WaitForSeconds(duration);
        SubtractValue(value);
        AttributeUI.Instance.UpdateAttributeUIItemValue(this);

    }
    IEnumerator AddValueOverTimeWithHoldingRoutine(float value, float rampTime, float holdTime)
    {

        float i = 0.0f;
        float rate = 1.0f / rampTime;
        float lastAdded = 0;
        while (i <= 1)
        {
            i += Time.deltaTime * rate;
            float current = Mathf.Lerp(0, value, i);
            _value += current - lastAdded;
            lastAdded = current;
            AttributeUI.Instance.UpdateAttributeUIItemValue(this);
            yield return null;
        }
        _value += value - lastAdded;
        AttributeUI.Instance.UpdateAttributeUIItemValue(this);
        yield return new WaitForSeconds(holdTime);
        SubtractValue(value);
        AttributeUI.Instance.UpdateAttributeUIItemValue(this);

    }
    IEnumerator AddValuePerSecondRoutine(float valuePerSecond, float duration)
    {

      
        float currentTime = Time.time;

        while (Time.time - currentTime <= duration)
        {
            _value += valuePerSecond;
            AttributeUI.Instance.UpdateAttributeUIItemValue(this);
            yield return new WaitForSeconds(1);
           
        }
        //_value += _value - (duration* valuePerSecond);
        AttributeUI.Instance.UpdateAttributeUIItemValue(this);

    }
}

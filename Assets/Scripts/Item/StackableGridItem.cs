﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackableGridItem : GridItem
{
    public Text StackNumber;
    private int _currentNumber;
    private int _maxNumber;
    
    public override void Setup(Item item)
    {
        base.Setup(item);
        _maxNumber =ItemsDatabaseBahaviour.Instance.GetItemTypeMaxStackCount(_item.ItemType);
        SetImage();
        SetCurrentNubmber(1);
    }
    private void SetImage()
    {
        GridIcon.sprite = _item.Image;
    }
    public void SetCurrentNubmber(int number)
    {
        string numberTxt = string.Empty;
        numberTxt = number.ToString() + " / " + _maxNumber.ToString();
        StackNumber.text = numberTxt;
        _currentNumber = number;
    }
    public bool IsItemAtTheMax()
    {
        if (_currentNumber == _maxNumber)
        {
            return true;
        }
        else
            return false;
    }
    public int GetItemCount()
    {
        return _currentNumber;
    }

}
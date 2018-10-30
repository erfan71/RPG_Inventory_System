﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridItem : ItemBehaviour, IPointerClickHandler {

    public Image GridIcon;
    private GeneralPanel parentPanel;
    protected int _currentNumber;

    private enum State
    {
        InTheAir,
        Grounded
    }
    private State _state;
    public override void Setup(Item item)
    {
        base.Setup(item);
        SetImage();
        _state = State.Grounded;
        parentPanel = GetComponentInParent<GeneralPanel>();
        GetComponent<Image>().raycastTarget = true;
        _currentNumber = 1;

    }
    public int GetItemCount()
    {
        return _currentNumber;
    }
    private void SetImage()
    {
        GridIcon.sprite = _item.Image;
    }
    private void SetPosition(Vector2 screenPos)
    {
        transform.position = screenPos;
    }
    
    private void GoIntoTheAir()
    {
        _state = State.InTheAir;
        transform.SetParent( parentPanel.CanvasRoot.transform);
        StartCoroutine(InTheAirControl());
        GetComponent<Image>().raycastTarget = false;
        ItemUnderTheMouse.Instance.SetCurrentDragedItem(this);
   }

    private IEnumerator InTheAirControl()
    {
        while (_state == State.InTheAir)
        {
            SetPosition(Input.mousePosition);
            yield return null;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_state == State.Grounded)
            GoIntoTheAir();
      
    }
    public GeneralPanel GetContainedPanel()
    {
        return parentPanel;
    }
   
}

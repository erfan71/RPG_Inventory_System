using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridItem : ItemBehaviour, IPointerClickHandler {

    public Image GridIcon;
    private Vector2 defaultScale;
    private GeneralPanel parentPanel;
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
        defaultScale = transform.localScale;
        parentPanel = GetComponentInParent<GeneralPanel>();
        GetComponent<Image>().raycastTarget = true;

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
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupableObject : ItemBehaviour, IPointerClickHandler {

    public int ItemId;

    public SpriteRenderer Renderer;
    public static System.Action<PickupableObject> OnPickupableItemClickedEvent;
    private void Start()
    {
        Setup(ItemId);
        SetSprite();
    }
    void SetSprite()
    {
        Renderer.sprite = _item.Image;
    }

    public Vector2 GetCenterPosition()
    {
        return transform.position;
    }

  
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        OnPickupableItemClickedEvent?.Invoke(this);
    }
}

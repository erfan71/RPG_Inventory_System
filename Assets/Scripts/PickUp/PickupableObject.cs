using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : ItemBehaviour {

    public int ItemId;

    public SpriteRenderer Renderer;
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
   
}

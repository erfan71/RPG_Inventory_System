using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridItem : ItemBehaviour {

    public Image GridIcon;
    public override void Setup(Item item)
    {
        base.Setup(item);
        SetImage();

    }
    private void SetImage()
    {
        GridIcon.sprite = _item.Image;
    }
}

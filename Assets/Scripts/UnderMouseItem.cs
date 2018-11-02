using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnderMouseItem : MonoBehaviour {

    public Image Icon;

    public void Setup(Sprite iconSprite)
    {
        Icon.sprite = iconSprite;
    }

   
}

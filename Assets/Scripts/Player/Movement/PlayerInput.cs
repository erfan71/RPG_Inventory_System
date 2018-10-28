using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class handle inputs based on the different platforms for the player
/// </summary>
public class PlayerInput : MonoBehaviour
{
    private float _horizontal;
    private float _Vertical;
   
    public string myString { get; set; }

    void Awake()
    {

    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _Vertical = Input.GetAxis("Vertical");
    }
    public float GetHorizontal()
    {
        return _horizontal;
    }
    public float GetVertical()
    {
        return _Vertical;
    }

}

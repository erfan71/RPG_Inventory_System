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

    public Joystick Joystick;
   
    public string myString { get; set; }

    void Awake()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR) || INPUT_DEBUG
        _horizontal = Joystick.Horizontal;
        _Vertical = Joystick.Vertical;
        Joystick.gameObject.SetActive(true);

#else
        Joystick.gameObject.SetActive(false);
#endif
    }

    void Update()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR) || INPUT_DEBUG
        _horizontal = Joystick.Horizontal;
        _Vertical = Joystick.Vertical;
#else 
        _horizontal = Input.GetAxis("Horizontal");
        _Vertical = Input.GetAxis("Vertical");
       
#endif
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

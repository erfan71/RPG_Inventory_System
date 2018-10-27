using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour {

    public Animator Animator;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;

    private const string HORIZONTAL_VALUE_PARAMETER_NAME = "HorizontalValue";
    private const string HORIZONTAL_STATE_PARAMETER_NAME = "Horizontal";
    private const string VERTICAL_VALUE_PARAMETER_NAME = "VerticalValue";
    private const string VERTICAL_STATE_PARAMETER_NAME = "Vertical";
    private const string SPEED_MAGNITUDE_PARAMETER_NAME = "SpeedMagnitude";


    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();

    }
    void Start () {
        

    }
	
	// Update is called once per frame
	void LateUpdate () {

        float horizontalValue = _playerInput.GetHorizontal();
        float verticalValue = _playerInput.GetVertical();

        if (horizontalValue == 0)
            Animator.SetBool(HORIZONTAL_STATE_PARAMETER_NAME, false);
        else
            Animator.SetBool(HORIZONTAL_STATE_PARAMETER_NAME, true);
        if (verticalValue == 0)
            Animator.SetBool(VERTICAL_STATE_PARAMETER_NAME, false);
        else
            Animator.SetBool(VERTICAL_STATE_PARAMETER_NAME, true);

        Animator.SetFloat(HORIZONTAL_VALUE_PARAMETER_NAME, horizontalValue);
        Animator.SetFloat(VERTICAL_VALUE_PARAMETER_NAME, verticalValue);
        Animator.SetFloat(SPEED_MAGNITUDE_PARAMETER_NAME,  Mathf.Max(Mathf.Abs( horizontalValue), Mathf.Abs( verticalValue)));
    }
}

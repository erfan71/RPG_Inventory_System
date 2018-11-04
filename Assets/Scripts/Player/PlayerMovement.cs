using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handle movement of the player based on the rigidbody that is assigned.
/// </summary>

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D RigidBody;
    public float SpeedScale;

    private PlayerInput _playerInput;

    public static System.Action On10UnitMove;

    private Vector3 _lastPosition;
    private const int DISTANCE_TO_SEND_MOVE_EVENT = 10;
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _lastPosition = GetPosition();
    }
    private void Update()
    {
        if (Vector2.Distance( GetPosition(), _lastPosition) >= DISTANCE_TO_SEND_MOVE_EVENT)
        {
            _lastPosition = GetPosition();
            On10UnitMove?.Invoke();
        }
    }
    void FixedUpdate()
    {
        RigidBody.velocity = new Vector2(_playerInput.GetHorizontal(), _playerInput.GetVertical()).normalized* SpeedScale;      
    }
    public float GetVelocityMagnitude()
    {
        return Mathf.Max(Mathf.Abs( _playerInput.GetHorizontal()), Mathf.Abs( _playerInput.GetVertical())) * SpeedScale;
    }
    public Vector2 GetPosition()
    {
        return RigidBody.transform.position;
    }
    public Vector2 GetVelocityDirection()
    {
        return RigidBody.velocity.normalized;
    } 
}

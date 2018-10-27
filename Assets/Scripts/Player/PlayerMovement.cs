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

    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {


    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        RigidBody.velocity = new Vector2(_playerInput.GetHorizontal(), _playerInput.GetVertical());
    }
}

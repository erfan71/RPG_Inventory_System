using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    // Use this for initialization
    public PlayerMovement Player;
    public Collider2D _mapBoundry;

    private Vector2 _lastPos;
    private float _cameraWidthHalf;
    private float _cameraHeightHalf;
    void Start()
    {
        _lastPos = Player.GetPosition();
        _cameraHeightHalf = Camera.main.orthographicSize;
        _cameraWidthHalf = _cameraHeightHalf * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 currentPlayerPos = Player.GetPosition();
        Vector2 positionDiff = currentPlayerPos - _lastPos;

        Vector3 currentCameraPos = transform.position;
        Vector2 nextCameraPos = currentCameraPos;

        nextCameraPos.x = Mathf.Clamp(currentCameraPos.x + positionDiff.x, _mapBoundry.bounds.min.x + _cameraWidthHalf, _mapBoundry.bounds.max.x - _cameraWidthHalf);
        nextCameraPos.y = Mathf.Clamp(currentCameraPos.y + positionDiff.y, _mapBoundry.bounds.min.y + _cameraHeightHalf, _mapBoundry.bounds.max.y - _cameraHeightHalf);

        transform.position = new Vector3(nextCameraPos.x, nextCameraPos.y, currentCameraPos.z);
        _lastPos = currentPlayerPos;
    }
}

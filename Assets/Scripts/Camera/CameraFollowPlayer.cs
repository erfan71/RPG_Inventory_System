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
        Vector2 currentCameraPos = transform.position;
        Vector2 positionDiff = currentPlayerPos - _lastPos;
        _lastPos = currentPlayerPos;
        if (positionDiff.sqrMagnitude == 0) return;

        float cameraRightAncher = currentCameraPos.x + _cameraWidthHalf * 0.5f;
        float cameraLeftAncher = currentCameraPos.x - _cameraWidthHalf * 0.5f;
        float cameraTopAncher = currentCameraPos.y + _cameraHeightHalf * 0.5f;
        float cameraDownAncher = currentCameraPos.y - _cameraHeightHalf * 0.5f;

        bool cameraFollow = true;

        if (currentPlayerPos.x <= cameraLeftAncher)
        {
            if (positionDiff.x > 0)
            {
                cameraFollow = false;
            }
        }
        else if (currentPlayerPos.x >= cameraRightAncher)
        {
            if (positionDiff.x < 0)
            {
                cameraFollow = false;
            }
        }
        if (currentPlayerPos.y <= cameraDownAncher)
        {
            if (positionDiff.y > 0)
            {
                cameraFollow = false;
            }
        }
        else if (currentPlayerPos.y >= cameraTopAncher)
        {
            if (positionDiff.y < 0)
            {
                cameraFollow = false;
            }
        }

        if (!cameraFollow) return;

        Vector2 nextCameraPos = currentCameraPos;

        nextCameraPos.x = Mathf.Clamp(currentCameraPos.x + positionDiff.x, _mapBoundry.bounds.min.x + _cameraWidthHalf, _mapBoundry.bounds.max.x - _cameraWidthHalf);
        nextCameraPos.y = Mathf.Clamp(currentCameraPos.y + positionDiff.y, _mapBoundry.bounds.min.y + _cameraHeightHalf, _mapBoundry.bounds.max.y - _cameraHeightHalf);

        transform.position = new Vector3(nextCameraPos.x, nextCameraPos.y, transform.position.z);
    }
}

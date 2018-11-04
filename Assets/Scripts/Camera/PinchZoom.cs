using UnityEngine;
/// <summary>
/// Most of the code are adopted from here: https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
/// </summary>
public class PinchZoom : MonoBehaviour
{
    public float ZoomSpeedTouch = 0.1f;
    public float ZoomSpeedMouse = 0.5f;
    public float MinZoom;
    public float MaxZoom;
    private Vector2[] _lastZoomPositions; // Touch mode only
    private bool _wasZoomingLastFrame; // Touch mode only

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        _wasZoomingLastFrame = false;
    }

    void Update()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR) || INPUT_DEBUG

        HandleTouch();
#else

        HandleMouse();
#endif
    }
    void HandleTouch()
    {
        if (Input.touchCount == 2)
        {
            Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };


            if (!_wasZoomingLastFrame)
            {
                _lastZoomPositions = newPositions;
                _wasZoomingLastFrame = true;
            }
            else
            {
                if (Input.touches[0].phase==TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
                {
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(_lastZoomPositions[0], _lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);
                }
               

                _lastZoomPositions = newPositions;
            }         
        }     
    }

    void HandleMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - (offset * speed), MaxZoom, MinZoom);
        //Debug.Log("old: " + _camera.orthographicSize + " new: " + newOrthSize);
        //_camera.orthographicSize = Mathf.Clamp(newOrthSize, _camera.orthographicSize * 0.9f, _camera.orthographicSize * 1.1f);
    }
}
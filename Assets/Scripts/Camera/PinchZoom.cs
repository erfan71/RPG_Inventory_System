using UnityEngine;
/// <summary>
/// Most of the code are adopted from here: https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
/// </summary>
public class PinchZoom : MonoBehaviour
{
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public  float MinZoom = 7;
    public float MaxZoom = 2;
    private Camera _camera;
    float targetOrtho = 0;

    private void Start()
    {
        _camera = Camera.main;
        targetOrtho = _camera.orthographicSize;
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS

        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
           
                // ... change the orthographic size based on the change in distance between the touches.
           _camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, MaxZoom, MinZoom);
           
        }
#endif

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * orthoZoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, MaxZoom, MinZoom);
        }

        _camera.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, 2 * Time.deltaTime);

    }
}
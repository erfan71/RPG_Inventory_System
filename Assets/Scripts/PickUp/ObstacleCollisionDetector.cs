using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollisionDetector : MonoBehaviour {

    private const string OBSTACLE_TAG = "Obstacle";

    public static System.Action OnObstacleCollisionEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(OBSTACLE_TAG))
        {
            OnObstacleCollisionEvent?.Invoke();
        }
    }
}

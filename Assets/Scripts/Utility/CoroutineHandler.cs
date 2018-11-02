using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour {

    #region SingletonPattern
    private static CoroutineHandler _instance;
    public static CoroutineHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CoroutineHandler>();
            }
            return _instance;
        }
    }

    #endregion
}

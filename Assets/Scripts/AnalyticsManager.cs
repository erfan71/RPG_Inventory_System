using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;

public class AnalyticsManager : MonoBehaviour
{

    private const int NUMBER_MOVE_SEND = 5;
    private const float TIME_BETWEEN_OBTACLE_COLLISION_EVENT_SEND = 5;
    private int _numberOfMoveSent = 0;
    private float _lastTimeOfCollisionEventSent = 0;

    #region SingletonPattern
    private static AnalyticsManager _instance;
    public static AnalyticsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AnalyticsManager>();
            }
            return _instance;
        }
    }

    #endregion

    void Start()
    {
        OnGameStart();

        InventoryController.OnItemPickedUpEvent += ItemPickedUpToInventory;
        EquipmentController.OnItemEquipedEvent += OnItemEquipedCallBack;
        InventoryController.OnItemConsumed += OnItemConsumedCallBack;
        GeneralPanel.OnPanelOpened += OnPanelOpenedCallBack;
        PlayerMovement.On10UnitMove += On10UnitMoveCallBack;
        ObstacleCollisionDetector.OnObstacleCollisionEvent += OnObstacleCollisionCallBack;
    }

   

    private void OnDestroy()
    {
        InventoryController.OnItemPickedUpEvent -= ItemPickedUpToInventory;
        EquipmentController.OnItemEquipedEvent -= OnItemEquipedCallBack;
        InventoryController.OnItemConsumed -= OnItemConsumedCallBack;
        GeneralPanel.OnPanelOpened -= OnPanelOpenedCallBack;
        PlayerMovement.On10UnitMove -= On10UnitMoveCallBack;
        ObstacleCollisionDetector.OnObstacleCollisionEvent -= OnObstacleCollisionCallBack;


    }
    void Update()
    {

    }
    /// <summary>
    /// We assume that this scene just runs one-time per session. In a real scenario, we have to call this function in scenes that
    /// load just one time. like Splash screen.
    /// 
    /// IMPORTANT: we don't need time as a number type. because we don't want Unity to sum or average the number. 
    /// We want it to be categorized. So we need to send it as a string like "Hour20" representing 20:00. 
    /// Considering this, we don't want to have lots of items in the dashboards like "Time20:52", "Time20:53" and etc.
    /// It's about 24*60 state and it's not usable. So we send the date in this format:
    /// "hour20:00", "hour20:10". as you see all the minutes from 00 to 09 is categorized to 00. and all the minutes from 10 to 19
    /// is categorized to 10. based on this. we only have 24*10 state and we can track the most famous time for starting the game better.
    /// 
    /// </summary>
    void OnGameStart()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("platform", Application.platform.ToString());
        param.Add("time", "hour: " + currentTime.Hours + ":" + (currentTime.Minutes/10).ToString("00"));

        Analytics.CustomEvent("GameStart", param);
    }
    void ItemPickedUpToInventory(Item item)
    {
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("name", item.Name);
        param.Add("type", item.ItemType.ToString());

        Analytics.CustomEvent("ItemPickedUp", param);
    }
    private void OnItemEquipedCallBack(Item item)
    {
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("name", item.Name);
        param.Add("slot", item.Equipment.ToString() );

        Analytics.CustomEvent("ItemEquiped", param);
    }
    private void OnItemConsumedCallBack(Item item)
    {
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("name", item.Name);
        param.Add("type", item.ItemType.ToString());

        Analytics.CustomEvent("ItemConsumed", param);
    }
    private void OnPanelOpenedCallBack(string obj)
    {
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("name", obj);

        Analytics.CustomEvent("OnPanleOpened", param);

    }
    private void On10UnitMoveCallBack()
    {
        if (_numberOfMoveSent < NUMBER_MOVE_SEND)
        {
            Analytics.CustomEvent("10UnitMove");
            _numberOfMoveSent++;

        }


    }
    private void OnObstacleCollisionCallBack()
    {
        if (Time.time - _lastTimeOfCollisionEventSent >= TIME_BETWEEN_OBTACLE_COLLISION_EVENT_SEND)
        {
            _lastTimeOfCollisionEventSent = Time.time;
            Analytics.CustomEvent("ObstacleCollision");
        }
    }
}
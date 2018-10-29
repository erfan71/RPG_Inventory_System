using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Singleton
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ObjectPoolManager>();
            }
            return instance;
        }
    }
    #endregion Singleton
    // Use this for initialization
    [System.Serializable]
    public struct PoolableObjectConfig
    {
        public string Key;
        public GameObject Prefab;
        public int Count;
        public PoolableObjectConfig(string key, GameObject prefab, int count)
        {
            this.Key = key;
            this.Prefab = prefab;
            this.Count = count;
        }
    }
   
    [Tooltip("How many object instantiate to the pool when there is nothing left")]
    public int DefaultRegenerateCount = 100;
    public List<PoolableObjectConfig> ObjectsDetail;
    private Dictionary<string, List<GameObject>> ObjectsPool;

    public void Awake()
    {
        ObjectsPool = new Dictionary<string, List<GameObject>>();
        foreach (PoolableObjectConfig obj in ObjectsDetail)
        {
            ObjectsPool.Add(obj.Key, InstantiateObjectForPool(obj));
        }

    }
    private List<GameObject> InstantiateObjectForPool(PoolableObjectConfig obj)
    {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < obj.Count; i++)
        {
            GameObject objInstance = Instantiate(obj.Prefab);
            objInstance.SetActive(false);
            objInstance.transform.SetParent(this.transform);
            objInstance.name = obj.Prefab.name;
            objInstance.AddComponent<PoolableObjectInstance>();
            PoolableObjectInstance poolableRef = objInstance.GetComponent<PoolableObjectInstance>();
            poolableRef.Key = obj.Key;
            poolableRef.UseStatus = PoolableObjectInstance.UsageStatus.Ready;
            objects.Add(objInstance);
        }
        return objects;
    }
    public T GetObject<T>(string key) where T : class
    {
        if (ObjectsPool.ContainsKey(key))
        {
            List<GameObject> objectWithThisKey = ObjectsPool[key];
            if (objectWithThisKey.Count > 1)
            {
                return PrepareItemtoExit(objectWithThisKey).GetComponent<T>();
            }
            else
            {
                PoolableObjectConfig objconf = new PoolableObjectConfig(key, objectWithThisKey[0], DefaultRegenerateCount);
                objectWithThisKey.AddRange(InstantiateObjectForPool(objconf));

                return PrepareItemtoExit(objectWithThisKey).GetComponent<T>();
            }
        }
        else
        {
            Debug.LogError("No Object With This Key");
            return null;
        }

    }
    GameObject PrepareItemtoExit(List<GameObject> selectedList)
    {
        GameObject temp = selectedList[1];
        temp.SetActive(true);
        temp.transform.SetParent(null);
        selectedList.RemoveAt(1);
        temp.GetComponent<PoolableObjectInstance>().UseStatus = PoolableObjectInstance.UsageStatus.InUse;
        return temp;
    }
    public void RecycleObject(PoolableObjectInstance poi)
    {
        if (poi.UseStatus == PoolableObjectInstance.UsageStatus.InUse)
        {
            poi.gameObject.SetActive(false);
            poi.transform.SetParent(this.transform);
            poi.UseStatus = PoolableObjectInstance.UsageStatus.Ready;
            ObjectsPool[poi.Key].Add(poi.gameObject);
        }

    }
}


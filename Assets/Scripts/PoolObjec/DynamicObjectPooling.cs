using UnityEngine;
using System.Collections.Generic;

public class DynamicObjectPooling : MonoBehaviour
{
    [SerializeField] private PoolConfigSO poolConfigSO;
    private Queue<PoolObject> poolQueue = new Queue<PoolObject>();

    private void Awake()
    {
        if (poolConfigSO != null)
        {
            for (int i = 0; i < poolConfigSO.initialSize; i++)
            {
                AddObjectToPool();
            }
        }
        else
        {
            Debug.LogError("PoolConfigSO no está asignado en DynamicObjectPooling.");
        }
    }

    private PoolObject AddObjectToPool()
    {
        PoolObject obj = Instantiate(poolConfigSO.prefab, transform);
        obj.OnDeactivate();
        poolQueue.Enqueue(obj);
        return obj;
    }

    public PoolObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            PoolObject obj = poolQueue.Dequeue();
            obj.OnActivate();
            return obj;
        }
        Debug.Log("Te acabaste los objetos inactivos creando mas...");
        PoolObject newObj = AddObjectToPool();
        newObj.OnActivate();
        return newObj;
    }

    public void ReturnObject(PoolObject obj)
    {
        obj.OnDeactivate();
        poolQueue.Enqueue(obj);
    }
}
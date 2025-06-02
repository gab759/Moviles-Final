using System.Collections.Generic;
using UnityEngine;
using System;

public class DinamicObjectPooling : MonoBehaviour
{
    public PoolConfigSO config;

    private Queue<PoolObject> pool = new Queue<PoolObject>();

    public static event Action<PoolObject> OnObjectTakenFromPool;
    public static event Action<PoolObject> OnObjectReturnedToPool;
    public static event Action<PoolObject> OnObjectCreated;

    private void Awake()
    {
        if (config == null)
        {
            Debug.LogError("PoolConfigSO no asignado en DinamicObjectPooling.");
            return;
        }

        for (int i = 0; i < config.initialSize; i++)
        {
            AddNewObjectToPool();
        }
    }

    private void AddNewObjectToPool()
    {
        PoolObject obj = Instantiate(config.prefab, transform);
        obj.OnDespawn();
        obj.GetComponent<PoolObjectDynamic>().Initialize(this);
        pool.Enqueue(obj);
        OnObjectCreated?.Invoke(obj);
    }

    public PoolObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            AddNewObjectToPool();
        }

        PoolObject obj = pool.Dequeue();
        obj.OnSpawn();
        OnObjectTakenFromPool?.Invoke(obj);
        return obj;
    }

    public void ReturnToPool(PoolObject obj)
    {
        obj.OnDespawn();
        pool.Enqueue(obj);
        OnObjectReturnedToPool?.Invoke(obj);
    }
}
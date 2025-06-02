using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectPooling : MonoBehaviour
{
    public PoolConfigSO config;

    private Queue<PoolObject> pool = new Queue<PoolObject>();

    public static event Action<PoolObject> OnObjectTakenFromPool;
    public static event Action<PoolObject> OnObjectReturnedToPool;

    private void Awake()
    {
        if (config == null)
        {
            Debug.LogError("PoolConfigSO no asignado en StaticObjectPooling.");
            return;
        }

        for (int i = 0; i < config.initialSize; i++)
        {
            PoolObject obj = Instantiate(config.prefab, transform);
            obj.OnDespawn();
            obj.GetComponent<PoolObjectStatic>().Initialize(this);
            pool.Enqueue(obj);
        }
    }

    public PoolObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("El pool estático está vacío.");
            return null;
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
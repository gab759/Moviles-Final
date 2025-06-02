using System;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public static event Action<PoolObject> OnObjectSpawned;
    public static event Action<PoolObject> OnObjectDespawned;

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
        OnObjectSpawned?.Invoke(this);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
        OnObjectDespawned?.Invoke(this);
    }

    public abstract void ReturnToPool();
}
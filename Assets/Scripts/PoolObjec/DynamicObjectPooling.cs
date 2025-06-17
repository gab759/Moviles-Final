using UnityEngine;
using System.Collections.Generic;

public class DynamicObjectPooling<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] private PoolConfigSO poolConfigSO;
    private Queue<T> poolQueue = new Queue<T>();

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

    private T AddObjectToPool()
    {
        T obj = Instantiate(poolConfigSO.prefab as T, transform);
        obj.OnDeactivate();
        poolQueue.Enqueue(obj);
        return obj;
    }

    public T GetObject()
    {
        if (poolQueue.Count > 0)
        {
            T obj = poolQueue.Dequeue();
            obj.OnActivate();
            return obj;
        }

        Debug.Log("Creando un objeto nuevo porque el pool está vacío...");
        T newObj = AddObjectToPool();
        newObj.OnActivate();
        return newObj;
    }

    public void ReturnObject(T obj)
    {
        obj.OnDeactivate();
        poolQueue.Enqueue(obj);
    }
}
using UnityEngine;
using System.Collections.Generic;

public class StaticObjectPooling<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] private PoolConfigSO config;

    private Queue<T> availableObjects = new Queue<T>();

    private void Awake()
    {
        for (int i = 0; i < config.initialSize; i++)
        {
            T obj = Instantiate(config.prefab as T, transform);
            obj.OnDeactivate(); // Lo desactivamos
            availableObjects.Enqueue(obj); // Y lo ponemos en la cola
        }
    }

    public T GetObject()
    {
        if (availableObjects.Count == 0)
        {
            Debug.Log("No hay objetos inactivos disponibles en el pool estático.");
            return null;
        }

        T obj = availableObjects.Dequeue();

        obj.OnActivate();

        return obj;
    }

    public void ReturnObject(T obj)
    {
        obj.OnDeactivate();

        availableObjects.Enqueue(obj);
    }
}
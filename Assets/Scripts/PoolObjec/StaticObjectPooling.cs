using UnityEngine;
using System.Collections.Generic;

public class StaticObjectPooling : MonoBehaviour
{
    [SerializeField] private PoolConfigSO config;
    private List<PoolObject> pool = new List<PoolObject>();

    private void Awake()
    {
        for (int i = 0; i < config.initialSize; i++)
        {
            PoolObject obj = Instantiate(config.prefab, transform);
            obj.OnDeactivate();
            pool.Add(obj);
        }
    }

    public PoolObject GetObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.OnActivate();
                return obj;
            }
        }

        Debug.Log("No hay objetos inactivos disponibles en el pool estático.");
        return null;
    }

    public void ReturnObject(PoolObject obj)
    {
        if (pool.Contains(obj))
            obj.OnDeactivate();
        else
            Debug.LogWarning("Objeto no pertenece a este pool.");
    }
}


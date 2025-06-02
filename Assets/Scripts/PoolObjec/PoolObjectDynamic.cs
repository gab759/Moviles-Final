using UnityEngine;


public class PoolObjectDynamic : PoolObject
{
    private DinamicObjectPooling pool;

    public void Initialize(DinamicObjectPooling assignedPool)
    {
        pool = assignedPool;
    }

    public override void ReturnToPool()
    {
        pool?.ReturnToPool(this);
    }
}
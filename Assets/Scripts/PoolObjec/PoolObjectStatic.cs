using UnityEngine;


public class PoolObjectStatic : PoolObject
{
    private StaticObjectPooling pool;

    public void Initialize(StaticObjectPooling assignedPool)
    {
        pool = assignedPool;
    }

    public override void ReturnToPool()
    {
        pool?.ReturnToPool(this);
    }
}
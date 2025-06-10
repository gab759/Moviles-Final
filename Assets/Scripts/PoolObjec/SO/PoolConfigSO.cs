using UnityEngine;


[CreateAssetMenu(fileName = "PoolConfig", menuName = "Scriptable Objects/Pool Config", order = 2)]
public class PoolConfigSO : ScriptableObject
{
    public PoolObject prefab;
    public int initialSize = 10;
}
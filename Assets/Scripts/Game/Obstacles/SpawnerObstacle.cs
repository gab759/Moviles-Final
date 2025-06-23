using UnityEngine;
using System.Collections;

public class SpawnerObstacle : MonoBehaviour
{
  /*  [Header("Pool")]
   // [SerializeField] private StaticObjectPooling pool ;

    [Header("Grid Settings")]
    [SerializeField] private int columnCount = 10;
    [SerializeField] private int rowCount = 6;
    [SerializeField] private float spacing = 1.5f;

    [Header("Base")]
    [SerializeField] private GameObject basePrefab;
    [SerializeField] private float baseYOffset = -0.5f;

    [Header("Timing")]
    [SerializeField] private float delayBetweenSpawns = 0.05f;
    [SerializeField] private float delayBetweenWaves = 1f;
    [SerializeField] private float activeDuration = 1f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Coroutine spawnRoutine;

    private void Start()
    {
        SpawnBaseTiles();

        spawnRoutine = StartCoroutine(SpawnObstaclesLoop());
    }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void SpawnBaseTiles()
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < columnCount; col++)
            {
                Vector3 offset = GetGridOffset(col, row);
                Vector3 basePos = transform.position + offset + new Vector3(0f, baseYOffset, 0f);
                Instantiate(basePrefab, basePos, Quaternion.identity, transform);
            }
        }
    }

    private IEnumerator SpawnObstaclesLoop()
    {
        while (true)
        {
            // Izquierda
            for (int row = rowCount - 1; row >= 0; row--)
            {
                for (int col = 0; col < columnCount / 2; col++)
                {
                    SpawnAndSchedule(col, row);
                }
                yield return new WaitForSeconds(delayBetweenSpawns);
            }

            yield return new WaitForSeconds(0.5f);

            // Derecha
            for (int row = rowCount - 1; row >= 0; row--)
            {
                for (int col = columnCount / 2; col < columnCount; col++)
                {
                    SpawnAndSchedule(col, row);
                }
                yield return new WaitForSeconds(delayBetweenSpawns);
            }

            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }

    private void SpawnAndSchedule(int col, int row)
    {
        Vector3 offset = GetGridOffset(col, row);
        Vector3 pos = transform.position + offset;

        PoolObject obj = pool.GetObject();
        if (obj == null) return;

        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.identity;

        StartCoroutine(DeactivateAfterTime(obj, activeDuration));
    }

    private IEnumerator DeactivateAfterTime(PoolObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        pool.ReturnObject(obj);
    }

    private Vector3 GetGridOffset(int col, int row)
    {
        float xOffset = (col - (columnCount - 1) / 2f) * spacing;
        float zOffset = (row - (rowCount - 1) / 2f) * spacing;
        return new Vector3(xOffset, 0f, zOffset);
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }*/
}
using UnityEngine;
using System.Collections;

public class RowSpawner : MonoBehaviour
{
  /*  [Header("Pool")]
    [SerializeField] private StaticObjectPooling pool;

    [Header("Grid Settings")]
    [SerializeField] private int columnCount = 10;
    [SerializeField] private int rowCount = 6;
    [SerializeField] private float spacing = 1.5f;

    [Header("Base")]
    [SerializeField] private GameObject basePrefab;
    [SerializeField] private float baseYOffset = -0.5f; // Cuánto más abajo colocar la base respecto al obstáculo

    [Header("Timing")]
    [SerializeField] private float delayBetweenSpawns = 0.05f;
    [SerializeField] private float delayBetweenWaves = 1f;
    [SerializeField] private float activeDuration = 1f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Coroutine spawnRoutine;

    private void Start()
    {
        // Instancia una vez la base debajo de cada punto
        SpawnBaseTiles();

        // Comienza el loop de obstáculos
        spawnRoutine = StartCoroutine(SpawnRowsLoop());
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

    private IEnumerator SpawnRowsLoop()
    {
        while (true)
        {
            // Generar una fila a la vez
            for (int row = 0; row < rowCount; row++)
            {
                SpawnRow(row);  // Genera la fila
                yield return new WaitForSeconds(delayBetweenSpawns);  // Pausa entre filas
            }

            yield return new WaitForSeconds(delayBetweenWaves);  // Espera entre oleadas
        }
    }

    private void SpawnRow(int row)
    {
        // Generar las columnas de esa fila
        for (int col = 0; col < columnCount; col++)
        {
            SpawnAndSchedule(col, row);
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
        float xOffset = (row - (rowCount - 1) / 2f) * spacing;  // Generamos el offset en el eje X para las filas
        float zOffset = (col - (columnCount - 1) / 2f) * spacing;  // El offset de las columnas se genera en Z
        return new Vector3(xOffset, 0f, zOffset);
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }*/
}

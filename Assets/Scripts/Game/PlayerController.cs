using UnityEngine;

public class PlayerController : PoolObject
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 touchDelta;

    public float moveSpeed = 5f;
    public int score = 0;

    [Header("Object Pool")]
    [SerializeField] private DynamicObjectPooling objectPool;

    [Header("Spawn Settings")]
    public GameObject targetObject;
    public float spawnRadius = 1f;
    public float spawnDistance = 1.5f;
    private float lastSpawnTime = 0f;
    private bool canSpawnCubes = false;

    private void Start()
    {
        if (objectPool == null)
        {
            Debug.LogError("El objeto ObjectPool no está asignado en PlayerController.");
        }

        if (targetObject == null)
        {
            Debug.LogError("El GameObject objetivo no está asignado.");
        }
    }

    private void Update()
    {
        HandleTouchInput();

        if (canSpawnCubes && Time.time - lastSpawnTime > 0.5f)
        {
            GenerateCubes(1);  // 
            lastSpawnTime = Time.time;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    touchDelta = touchEndPos - touchStartPos;

                    transform.position = new Vector3(
                        Mathf.Clamp(transform.position.x + touchDelta.x * moveSpeed * Time.deltaTime, -5f, 5f),
                        transform.position.y,
                        transform.position.z
                    );

                    touchStartPos = touch.position;
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        GenerateCubes(amount);
        Debug.Log("Nuevo puntaje (Suma): " + score);
    }

    public void MultiplyScore(int amount)
    {
        score *= amount;
        GenerateCubes(amount);
        Debug.Log("Nuevo puntaje (Multiplicación): " + score);
    }

    private void GenerateCubes(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();

            PoolObject newCube = objectPool.GetObject();
            newCube.transform.position = spawnPosition;
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 targetPos = targetObject.transform.position;

        float randomX = Random.Range(-spawnRadius, spawnRadius);
        float randomZ = Random.Range(-spawnRadius, spawnRadius);

        Vector3 spawnPosition = new Vector3(targetPos.x + randomX, targetPos.y, targetPos.z + randomZ);

        return spawnPosition;
    }

    public void SetObjectPool(DynamicObjectPooling pool)
    {
        objectPool = pool;
    }

    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }

    public void EnableCubeSpawn()
    {
        canSpawnCubes = true;
    }

    public void DisableCubeSpawn()
    {
        canSpawnCubes = false;
    }

}

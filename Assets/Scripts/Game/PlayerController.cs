using UnityEngine;

public class PlayerController : PoolObject
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 touchDelta;

    public float moveSpeed = 5f;
    public int score = 0;  // Puntaje del jugador

    [Header("Object Pool")]
    [SerializeField] private DynamicObjectPooling objectPool;  // El pool dinámico de cubos

    [Header("Spawn Settings")]
    public GameObject targetObject;  // El GameObject alrededor del cual los cubos se generarán y mantendrán
    public float spawnRadius = 1f;  // Radio de distancia a partir del objetivo donde se generarán los cubos
    public float spawnDistance = 1.5f;  // Distancia mínima entre los cubos generados
    private float lastSpawnTime = 0f;  // Temporizador de spawn para evitar crear cubos excesivamente rápido
    private bool canSpawnCubes = false;  // Controla si los cubos pueden generarse o no

    private void Start()
    {
        // Verificamos si el pool fue asignado
        if (objectPool == null)
        {
            Debug.LogError("El objeto ObjectPool no está asignado en PlayerController.");
        }

        // Si el targetObject no ha sido asignado, lo asignamos por código
        if (targetObject == null)
        {
            Debug.LogError("El GameObject objetivo no está asignado.");
        }
    }

    private void Update()
    {
        HandleTouchInput();  // Movimiento con el dedo

        // Solo genera cubos si `canSpawnCubes` es verdadero
        if (canSpawnCubes && Time.time - lastSpawnTime > 0.5f)  // Esto controla la frecuencia de generación de los cubos
        {
            GenerateCubes(1);  // Genera un cubo cada vez que pasen 0.5 segundos (ajustar según necesites)
            lastSpawnTime = Time.time;
        }
    }

    // Mueve el cubo con el dedo de izquierda a derecha
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

    // Lógica para la suma del puntaje
    public void IncreaseScore(int amount)
    {
        score += amount;
        GenerateCubes(amount);  // Generar cubos por el valor de la suma
        Debug.Log("Nuevo puntaje (Suma): " + score);
    }

    // Lógica para la multiplicación del puntaje
    public void MultiplyScore(int amount)
    {
        score *= amount;
        GenerateCubes(amount);  // Generar cubos según el multiplicador
        Debug.Log("Nuevo puntaje (Multiplicación): " + score);
    }

    // Generar cubos cerca del objetivo y mantenerlos juntos
    private void GenerateCubes(int amount)
    {
        // Generar cubos cerca del objetivo
        for (int i = 0; i < amount; i++)
        {
            // Calcular la posición de los cubos cerca del objetivo
            Vector3 spawnPosition = CalculateSpawnPosition();

            // Obtener un cubo del pool
            PoolObject newCube = objectPool.GetObject();
            newCube.transform.position = spawnPosition;  // Asignamos la nueva posición
        }
    }

    // Calcular la posición de los cubos cerca del GameObject objetivo
    private Vector3 CalculateSpawnPosition()
    {
        // Obtener la posición del objetivo
        Vector3 targetPos = targetObject.transform.position;

        // Generar una posición aleatoria alrededor del objetivo dentro del radio especificado
        float randomX = Random.Range(-spawnRadius, spawnRadius);
        float randomZ = Random.Range(-spawnRadius, spawnRadius);

        // Calculamos la nueva posición con una pequeña variabilidad en la X y Z, pero siempre cerca del objetivo
        Vector3 spawnPosition = new Vector3(targetPos.x + randomX, targetPos.y, targetPos.z + randomZ);

        return spawnPosition;
    }

    // Método para asignar el ObjectPool
    public void SetObjectPool(DynamicObjectPooling pool)
    {
        objectPool = pool;
    }

    // Método para asignar el targetObject desde otro script
    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }

    // Método para habilitar la generación de cubos al interactuar con un portal
    public void EnableCubeSpawn()
    {
        canSpawnCubes = true;
    }

    // Método para deshabilitar la generación de cubos (si se desea)
    public void DisableCubeSpawn()
    {
        canSpawnCubes = false;
    }

    // Detecta la colisión con objetos con el tag "Enemy" y destruye el cubo
    private void OnCollisionEnter(Collision collision)
    {
        // Si el cubo colisiona con un objeto que tenga el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir el cubo
            Destroy(gameObject);
        }
    }
}

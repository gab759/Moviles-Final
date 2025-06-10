using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private DynamicObjectPooling objectPool;  // Pool dinámico para los cubos

    [Header("Player Settings")]
    [SerializeField] private GameObject playerPrefab;  // Prefab del jugador
    [SerializeField] private GameObject targetObject;  // El objetivo al que deben estar cerca los jugadores

    private void Start()
    {
        // Crear un jugador, puedes crear más si lo deseas
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Asignamos el pool dinámico al jugador
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetObjectPool(objectPool);

        // Asignamos el targetObject al jugador
        playerController.SetTargetObject(targetObject);
    }
}

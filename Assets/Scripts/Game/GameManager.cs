using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private DynamicObjectPooling objectPool;  // Pool din�mico para los cubos

    [Header("Player Settings")]
    [SerializeField] private GameObject playerPrefab;  // Prefab del jugador
    [SerializeField] private GameObject targetObject;  // El objetivo al que deben estar cerca los jugadores

    private void Start()
    {
        // Crear un jugador, puedes crear m�s si lo deseas
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Asignamos el pool din�mico al jugador
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetObjectPool(objectPool);

        // Asignamos el targetObject al jugador
        playerController.SetTargetObject(targetObject);
    }
}

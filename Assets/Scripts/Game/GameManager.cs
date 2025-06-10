using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private DynamicObjectPooling objectPool;

    [Header("Player Settings")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject targetObject;

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(-0.75f, -0.787f, -6.28f), Quaternion.identity);

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetObjectPool(objectPool);

        playerController.SetTargetObject(targetObject);
        playerController.Init();
    }
}

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
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetObjectPool(objectPool);

        playerController.SetTargetObject(targetObject);
    }
}

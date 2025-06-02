using UnityEngine;

public class PlayerDuplicator : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnRoot;

    public void Duplicate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Instantiate(playerPrefab, spawnRoot.position + randomOffset, Quaternion.identity, spawnRoot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            Duplicate(5);
        }
    }
}
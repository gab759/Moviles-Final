using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PortalSO portalData;

    private Collider portalCollider;
    private TextMeshProUGUI portalText;

    private void Start()
    {
        portalData.GeneratePortal();
        portalCollider = GetComponent<Collider>();

        portalText = GetComponentInChildren<TextMeshProUGUI>();

        UpdatePortalText();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPortalEffect(other.gameObject);

            if (portalData.isRightPortal)
            {
                Destroy(gameObject);
            }
            else
            {
                DisableLeftPortalCollision();
            }
        }
    }

    private void ApplyPortalEffect(GameObject player)
    {
        if (portalData.portalType == PortalSO.PortalType.Suma)
        {
            player.GetComponent<PlayerController>().IncreaseScore(portalData.number);
        }
        else if (portalData.portalType == PortalSO.PortalType.Multiplicacion)
        {
            player.GetComponent<PlayerController>().MultiplyScore(portalData.number);
        }
    }

    private void DisableLeftPortalCollision()
    {
        if (portalCollider != null)
        {
            portalCollider.enabled = false;
        }
    }

    private void UpdatePortalText()
    {
        if (portalText != null)
        {
            string operationSymbol = portalData.portalType == PortalSO.PortalType.Suma ? "+" : "X";
            portalText.text = $"{operationSymbol} {portalData.number}";
        }
    }
}

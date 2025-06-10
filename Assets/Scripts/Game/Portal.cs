using UnityEngine;
using TMPro;  // Aseg�rate de importar TMP

public class Portal : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PortalSO portalData;  // Referencia al SO que contiene los datos del portal

    private Collider portalCollider;  // Referencia al Collider para desactivarlo
    private TextMeshProUGUI portalText;  // Referencia al componente TextMeshProUGUI del portal

    private void Start()
    {
        portalData.GeneratePortal();  // Genera el n�mero del portal
        portalCollider = GetComponent<Collider>();  // Guardamos la referencia del Collider

        // Intentamos obtener el componente TextMeshProUGUI en los hijos del portal
        portalText = GetComponentInChildren<TextMeshProUGUI>();

        // Actualizamos el texto del portal con el n�mero generado y su tipo de operaci�n
        UpdatePortalText();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);  // Mueve el portal hacia atr�s
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Si colisiona con el jugador
        {
            ApplyPortalEffect(other.gameObject);

            if (portalData.isRightPortal)  // Si es el portal derecho
            {
                Destroy(gameObject);  // Destruye el portal derecho
            }
            else  // Si es el portal izquierdo
            {
                DisableLeftPortalCollision();  // Desactiva la colisi�n del portal izquierdo
            }
        }
    }

    // Aplica la operaci�n de suma o multiplicaci�n
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

    // Desactiva la colisi�n del portal izquierdo
    private void DisableLeftPortalCollision()
    {
        if (portalCollider != null)
        {
            portalCollider.enabled = false;  // Desactiva la colisi�n pero mantiene el portal visible
        }
    }

    // Actualiza el texto del portal con el n�mero generado y su operaci�n
    private void UpdatePortalText()
    {
        if (portalText != null)
        {
            string operationSymbol = portalData.portalType == PortalSO.PortalType.Suma ? "+" : "X";
            portalText.text = $"{operationSymbol} {portalData.number}";  // Actualiza el texto
        }
    }
}

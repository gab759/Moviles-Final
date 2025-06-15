using UnityEngine;
using TMPro; 
public class Portal : MonoBehaviour
{
    [Header("Configuración (Desde el Proyecto)")]
    [SerializeField] private PortalSO portalData; 

    [Header("Componentes (Desde la Escena)")]
    [SerializeField] private TextMeshProUGUI textoDelPortal; 

    private int numeroActual;
    private PortalSO.PortalType tipoActual;

    void Awake()
    {
        ConfigurarPortal();
    }

    private void ConfigurarPortal()
    {
        if (portalData == null)
        {
            Debug.LogError("¡No hay PortalSO asignado en este portal!", this);
            return;
        }

        tipoActual = portalData.portalType;

        if (tipoActual == PortalSO.PortalType.Suma)
        {
            numeroActual = Random.Range(1, 11) * 5; 
            textoDelPortal.text = "+" + numeroActual;
        }
        else if (tipoActual == PortalSO.PortalType.Multiplicacion)
        {
            numeroActual = Random.Range(2, 5);
            textoDelPortal.text = "x" + numeroActual;
        }
    }

    public int GetNumero()
    {
        return numeroActual;
    }

    public PortalSO.PortalType GetTipo()
    {
        return tipoActual;
    }
}
using UnityEngine;
using TMPro;

public class JefeController : MonoBehaviour
{
    [Header("Datos (Desde el Proyecto)")]
    [SerializeField] private BossSO datosDelJefe;

    [Header("Componentes")]
    [SerializeField] private TextMeshPro textoVida;

    private int vidaActual;
    private Jefe miComponenteJefe; // Referencia al script para el pooling

    public int VidaActual { get { return vidaActual; } }

    void Awake()
    {
        miComponenteJefe = GetComponent<Jefe>();
    }

    // OnEnable se llama cada vez que el objeto es activado (perfecto para el pooling)
    void OnEnable()
    {
        // 1. Reseteamos la vida (esto ya lo teníamos)
        if (GameManager.Instance != null && datosDelJefe != null)
        {
            vidaActual = datosDelJefe.vida + GameManager.Instance.vidaAdicionalParaJefe;
        }
        else if (datosDelJefe != null)
        {
            vidaActual = datosDelJefe.vida;
        }
        ActualizarTexto();

        // --- 2. LÓGICA PARA REACTIVAR LOS COLLIDERS (LA SOLUCIÓN) ---
        // Buscamos todos los componentes Collider que tenga este objeto.
        Collider[] todosMisColliders = GetComponents<Collider>();

        // Usamos un bucle 'for' para recorrerlos.
        for (int i = 0; i < todosMisColliders.Length; i++)
        {
            // Y nos aseguramos de que cada uno esté activado.
            todosMisColliders[i].enabled = true;
        }
    }

    public void DerrotarJefe()
    {
        // ... (esta función se queda igual)
        Debug.Log("¡Jefe derrotado!");
        if (miComponenteJefe != null && miComponenteJefe.miPoolDeOrigen != null)
        {
            miComponenteJefe.miPoolDeOrigen.ReturnObject(miComponenteJefe);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ActualizarTexto()
    {
        // ... (esta función se queda igual)
        if (textoVida != null) textoVida.text = vidaActual.ToString();
    }
}
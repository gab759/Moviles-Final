using UnityEngine;
using TMPro;

public class JefeController : MonoBehaviour
{
    [Header("Datos (Desde el Proyecto)")]
    [SerializeField] private BossSO datosDelJefe; // Referencia a la plantilla de datos

    [Header("Componentes")]
    [SerializeField] private TextMeshPro textoVida;

    private int vidaActual;
    private Jefe miComponenteJefe; // Referencia al script para el pooling

    // Propiedad pública para que PlayerMovement pueda leer la vida
    public int VidaActual { get { return vidaActual; } }

    void Awake()
    {
        miComponenteJefe = GetComponent<Jefe>();
    }

    // OnEnable se llama cada vez que el objeto es activado (perfecto para el pooling)
    void OnEnable()
    {
        // Leemos la vida base del ScriptableObject y le SUMAMOS la vida adicional del GameManager.
        vidaActual = datosDelJefe.vida + GameManager.Instance.vidaAdicionalParaJefe;
        ActualizarTexto();
    }

    public void DerrotarJefe()
    {
        Debug.Log("¡Jefe derrotado!");

        // En lugar de desactivarse, se devuelve a su pool de origen
        if (miComponenteJefe != null && miComponenteJefe.miPoolDeOrigen != null)
        {
            miComponenteJefe.miPoolDeOrigen.ReturnObject(miComponenteJefe);
        }
        else
        {
            gameObject.SetActive(false); // Fallback
        }
    }

    private void ActualizarTexto()
    {
        if (textoVida != null)
        {
            textoVida.text = vidaActual.ToString();
        }
    }
}
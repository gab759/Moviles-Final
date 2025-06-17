using UnityEngine;

public class GeneradorDeNiveles : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("La lista de todos los 'Pools' de oleadas que hemos añadido a este objeto.")]
    [SerializeField] private OleadaPool[] listaDePoolsDeOleadas;

    [Tooltip("El objeto que marca dónde deben aparecer las nuevas oleadas.")]
    [SerializeField] private Transform puntoDeSpawn;

    [Tooltip("Cuántos segundos esperar entre la generación de una oleada y la siguiente.")]
    [SerializeField] private float tiempoEntreOleadas = 4f;
    
    [Header("Configuración del Jefe")]
    [SerializeField] private JefePool poolDelJefe; // El pool específico para el jefe
    [SerializeField] private int oleadasParaElJefe = 10;
    private float contadorDeTiempo;
    private int contadorDeOleadas;

    void Start()
    {
        contadorDeTiempo = tiempoEntreOleadas;
    }

    void Update()
    {
        contadorDeTiempo -= Time.deltaTime;

        if (contadorDeTiempo <= 0)
        {
            IntentarGenerarOleada();
            contadorDeTiempo = tiempoEntreOleadas;
        }
    }

    private void IntentarGenerarOleada()
    {
        contadorDeOleadas++; // Sumamos una oleada generada

        // 1. ¿Es hora de generar al jefe?
        if (contadorDeOleadas >= oleadasParaElJefe)
        {
            GenerarJefe();
            contadorDeOleadas = 0; // Reiniciamos el contador para la próxima vez
        }
        else
        {
            // 2. Si no, generamos una oleada normal
            GenerarOleadaNormal();
        }
    }
    private void GenerarOleadaNormal()
    {
        if (listaDePoolsDeOleadas.Length == 0) return;

        int indiceAleatorio = Random.Range(0, listaDePoolsDeOleadas.Length);
        OleadaPool poolElegido = listaDePoolsDeOleadas[indiceAleatorio];
        Oleada nuevaOleada = poolElegido.GetObject();

        if (nuevaOleada != null)
        {
            nuevaOleada.miPoolDeOrigen = poolElegido;
            nuevaOleada.transform.position = puntoDeSpawn.position;
            nuevaOleada.transform.rotation = puntoDeSpawn.rotation;
        }
    }

    private void GenerarJefe()
    {
        if (poolDelJefe == null) return;

        Jefe nuevoJefe = poolDelJefe.GetObject();

        if (nuevoJefe != null)
        {
            nuevoJefe.miPoolDeOrigen = poolDelJefe; // Le decimos al jefe a qué pool volver
            nuevoJefe.transform.position = puntoDeSpawn.position;
            nuevoJefe.transform.rotation = puntoDeSpawn.rotation;
        }
    }
}
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

    private float contadorDeTiempo;

    void Start()
    {
        contadorDeTiempo = tiempoEntreOleadas;
    }

    void Update()
    {
        contadorDeTiempo -= Time.deltaTime;

        if (contadorDeTiempo <= 0)
        {
            GenerarOleada();
            contadorDeTiempo = tiempoEntreOleadas;
        }
    }

    private void GenerarOleada()
    {
        if (listaDePoolsDeOleadas.Length == 0 || puntoDeSpawn == null)
        {
            Debug.LogWarning("El Generador de Niveles no tiene pools o un punto de spawn configurado.");
            return;
        }

        int indiceAleatorio = Random.Range(0, listaDePoolsDeOleadas.Length);
        OleadaPool poolElegido = listaDePoolsDeOleadas[indiceAleatorio];

        Oleada nuevaOleada = poolElegido.GetObject();
        if (nuevaOleada == null) return; 

        nuevaOleada.transform.position = puntoDeSpawn.position;
        nuevaOleada.transform.rotation = puntoDeSpawn.rotation;
    }
}
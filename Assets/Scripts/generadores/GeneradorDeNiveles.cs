using UnityEngine;

public class GeneradorDeNiveles : MonoBehaviour
{
    [Header("Configuraci�n")]
    [Tooltip("La lista de todos los 'Pools' de oleadas que hemos a�adido a este objeto.")]
    [SerializeField] private OleadaPool[] listaDePoolsDeOleadas;

    [Tooltip("El objeto que marca d�nde deben aparecer las nuevas oleadas.")]
    [SerializeField] private Transform puntoDeSpawn;

    [Tooltip("Cu�ntos segundos esperar entre la generaci�n de una oleada y la siguiente.")]
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
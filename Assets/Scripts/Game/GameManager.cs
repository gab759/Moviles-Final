using UnityEngine;

public class GameManager : MonoBehaviour
{
    // --- Inicio del Patrón Singleton ---
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Si ya existe una instancia y no soy yo, me destruyo.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Si no existe, me convierto en la instancia única.
            Instance = this;
            // Opcional: No destruir este objeto al cambiar de escena.
            // DontDestroyOnLoad(gameObject); 
        }
    }
    // --- Fin del Patrón Singleton ---


    // --- Nuestra Variable de Dificultad ---
    // Aquí guardaremos cuánta vida extra debe tener el próximo jefe.
    // Es pública para que otros scripts puedan leerla, pero solo el GameManager debería cambiarla.
    public int vidaAdicionalParaJefe = 0;
}
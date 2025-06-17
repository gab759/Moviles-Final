using UnityEngine;

public class GameManager : MonoBehaviour
{
    // --- Inicio del Patr�n Singleton ---
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
            // Si no existe, me convierto en la instancia �nica.
            Instance = this;
            // Opcional: No destruir este objeto al cambiar de escena.
            // DontDestroyOnLoad(gameObject); 
        }
    }
    // --- Fin del Patr�n Singleton ---


    // --- Nuestra Variable de Dificultad ---
    // Aqu� guardaremos cu�nta vida extra debe tener el pr�ximo jefe.
    // Es p�blica para que otros scripts puedan leerla, pero solo el GameManager deber�a cambiarla.
    public int vidaAdicionalParaJefe = 0;
}
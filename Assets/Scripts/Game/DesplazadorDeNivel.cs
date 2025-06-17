using UnityEngine;

public class DesplazadorDeNivel : MonoBehaviour
{
    // Velocidad a la que se mueven todos los objetos del nivel
    public static float velocidadDelJuego = 4f; // Usar� la velocidad que ten�as

    // Referencia al componente 'Oleada' para poder comunicarnos con el sistema de pooling
    private Oleada miComponenteOleada;

    // Awake se ejecuta una vez, cuando el objeto es activado.
    void Awake()
    {
        // Buscamos el componente Oleada en este mismo objeto.
        miComponenteOleada = GetComponent<Oleada>();
        if (miComponenteOleada == null)
        {
            Debug.LogError("�Este objeto de nivel no tiene el componente 'Oleada' y no podr� ser reciclado!", this);
        }
    }

    void Update()
    {
        // En cada frame, mueve el objeto hacia atr�s
        transform.Translate(Vector3.back * velocidadDelJuego * Time.deltaTime);

        // Si la oleada ya pas� de largo...
        if (transform.position.z < -18f)
        {
            // --- ESTE ES EL CAMBIO M�S IMPORTANTE ---
            // En lugar de Destroy(gameObject), hacemos lo siguiente:

            // Verificamos que todo est� en orden para evitar errores.
            if (miComponenteOleada != null && miComponenteOleada.miPoolDeOrigen != null)
            {
                // Le pedimos a su pool de origen que lo reciba de vuelta.
                // Esto lo desactiva y lo pone en la cola para ser reutilizado.
                miComponenteOleada.miPoolDeOrigen.ReturnObject(miComponenteOleada);
            }
            else
            {
                // Si algo fall� (ej. olvidamos asignar el pool de origen),
                // como plan B, simplemente lo desactivamos para que desaparezca.
                gameObject.SetActive(false);
            }
        }
    }
}
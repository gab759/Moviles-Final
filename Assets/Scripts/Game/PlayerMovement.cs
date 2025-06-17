using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuraci�n de la Multitud")]
    [SerializeField] private int columnas = 5; 
    [SerializeField] private float distanciaX = 1.2f; 
    [SerializeField] private float distanciaZ = 1.0f; 
    [Header("Dependencias")]
    [SerializeField] private GeneradorDeHamburguesas generador;
    private List<Hamburguesa> multitudDeHamburguesas = new List<Hamburguesa>();
    [SerializeField] private HamburguesaPool hamburguesaPool;
    [Header("Configuraci�n de Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float sensibilidad;
    [SerializeField] private float limiteIzquierdo = -4f;
    [SerializeField] private float limiteDerecho = 4f;

    private Vector3 posicionInicialTouch;
    private float posicionXInicialPlayer;
    void Start()
    {
        if (generador == null)
        {
            Debug.LogError("El Generador no est� asignado en PlayerMovement.", this);
        }

         A�adirHamburguesas(2); 
    }
    void Update()
    {
        // --- Movimiento con el Rat�n (para probar en PC) ---
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            Mover(mouseX * velocidadMovimiento);
        }

        // --- Movimiento T�ctil (para m�vil) ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Mover(touch.deltaPosition.x * 0.1f); 
        }
    }
    void LateUpdate()
    {
        ReorganizarMultitud();
    }
    private void Mover(float deltaX)
    {
        Vector3 nuevaPosicion = transform.position + new Vector3(deltaX, 0, 0) * Time.deltaTime;

        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);

        transform.position = nuevaPosicion;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            Portal portalChocado = other.GetComponent<Portal>();
            if (portalChocado != null)
            {
                int numero = portalChocado.GetNumero();
                PortalSO.PortalType tipo = portalChocado.GetTipo();

                if (tipo == PortalSO.PortalType.Suma)
                {
                    Debug.Log("Sumar " + numero + " hamburguesas!");
                    A�adirHamburguesas(numero);
                }
                else if (tipo == PortalSO.PortalType.Multiplicacion)
                {
                    Debug.Log("Multiplicar las hamburguesas por " + numero + "!");
                    int cantidadActual = multitudDeHamburguesas.Count;
                    int cantidadAAnadir = cantidadActual * (numero - 1);
                    A�adirHamburguesas(cantidadAAnadir);
                }

                other.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Jefe"))
        {
            JefeController jefe = other.GetComponentInParent<JefeController>();
            if (jefe != null)
            {
                IniciarBatallaConJefe(jefe);
                // Desactivamos el collider de detecci�n para no iniciar la batalla m�ltiples veces.
                other.enabled = false;
            }
        }
    }
    // Dentro de PlayerMovement.cs

    void IniciarBatallaConJefe(JefeController jefe)
    {
        int poderDelJugador = multitudDeHamburguesas.Count;
        int vidaDelJefe = jefe.VidaActual;

        Debug.Log("Iniciando batalla: Jugador(" + poderDelJugador + ") vs Jefe(" + vidaDelJefe + ")");

        if (poderDelJugador >= vidaDelJefe)
        {
            // --- �GANAMOS! ---
            RemoverMultiplesHamburguesas(vidaDelJefe);
            jefe.DerrotarJefe();

            // --- A�ADIMOS ESTA L�NEA ---
            // Le decimos al GameManager que incremente la dificultad para la pr�xima vez.
            GameManager.Instance.vidaAdicionalParaJefe += 10;

            Debug.Log("�El pr�ximo jefe ser� m�s fuerte! Vida adicional ahora: " + GameManager.Instance.vidaAdicionalParaJefe);
        }
        else
        {
            // --- PERDEMOS ---
            RemoverMultiplesHamburguesas(poderDelJugador); // Sacrificamos todas nuestras hamburguesas.
            Debug.Log("�GAME OVER! No tienes suficientes hamburguesas.");
            // Aqu� ir�a tu l�gica de fin de partida.
        }
    }

    void RemoverMultiplesHamburguesas(int cantidad)
    {
        // Hacemos el bucle a la inversa para evitar problemas al eliminar de una lista que se est� recorriendo.
        for (int i = 0; i < cantidad; i++)
        {
            // Verificamos que queden hamburguesas en la lista
            if (multitudDeHamburguesas.Count > 0)
            {
                // Tomamos la �ltima hamburguesa de la lista
                Hamburguesa hamburguesaARemover = multitudDeHamburguesas[multitudDeHamburguesas.Count - 1];
                // Y la removemos. La funci�n RemoverHamburguesa ya se encarga de devolverla al pool.
                RemoverHamburguesa(hamburguesaARemover);
            }
        }
    }

    private void A�adirHamburguesas(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            Vector3 posicionSpawn = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-2f, 0f));

            Hamburguesa nuevaHamburguesa = generador.GenerarHamburguesa(posicionSpawn);

            if (nuevaHamburguesa != null)
            {
                nuevaHamburguesa.crowdController = this;
                multitudDeHamburguesas.Add(nuevaHamburguesa);
            }
        }
        
        Debug.Log("Total de hamburguesas ahora: " + multitudDeHamburguesas.Count);
    }
    void ReorganizarMultitud()
    {
        float offsetCentrado = (columnas - 1) * distanciaX / 2;

        for (int i = 0; i < multitudDeHamburguesas.Count; i++)
        {
            int fila = i / columnas;
            int columna = i % columnas;
            float targetX = (columna * distanciaX) - offsetCentrado;
            float targetZ = -fila * distanciaZ;

            Vector3 posicionObjetivo = transform.position + new Vector3(targetX, 0, targetZ);

            multitudDeHamburguesas[i].MoverAPosicion(posicionObjetivo);
        }
    }
    public void RemoverHamburguesa(Hamburguesa hamburguesa)
    {
        // 1. Le pedimos al pool que se encargue de esta hamburguesa.
        // El pool la desactivar� y la guardar� para reciclarla.
        hamburguesaPool.ReturnObject(hamburguesa);

        // 2. La quitamos de nuestra lista de control.
        if (multitudDeHamburguesas.Contains(hamburguesa))
        {
            multitudDeHamburguesas.Remove(hamburguesa);
        }
        // 3. La llamada a ReorganizarMultitud() en LateUpdate se encargar� de rellenar el hueco.
        // Por eso la hemos quitado de aqu�.

    }
}
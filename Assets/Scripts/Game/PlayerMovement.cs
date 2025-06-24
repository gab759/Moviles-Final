using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de la Multitud")]
    [SerializeField] private int columnas = 5; 
    [SerializeField] private float distanciaX = 1.2f; 
    [SerializeField] private float distanciaZ = 1.0f;
    [SerializeField] private float desplazamientoZFormacion = -1.5f;
    [Header("Dependencias")]
    [SerializeField] private GeneradorDeHamburguesas generador;
    private List<Hamburguesa> multitudDeHamburguesas = new List<Hamburguesa>();
    private int contadorVirtualHamburguesas = 0; 

    [SerializeField] private HamburguesaPool hamburguesaPool;
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float sensibilidad;
    [SerializeField] private float limiteIzquierdo = -4f;
    [SerializeField] private float limiteDerecho = 4f;
    [SerializeField] private UIManager uiManager;
    private Vector3 posicionInicialTouch;
    private float posicionXInicialPlayer;

    [SerializeField] private Transform[] nodosDeSeguimiento;
    [SerializeField] private float velocidadSeguimientoNodos = 15f;
    void Start()
    {
        if (generador == null || hamburguesaPool == null || uiManager == null)
        {
            Debug.LogError("Alguna dependencia no está asignada en PlayerMovement.", this);
        }
        contadorVirtualHamburguesas = 1;
        AñadirHamburguesasFisicas(1);
        uiManager.ActualizarContadorHamburguesas(contadorVirtualHamburguesas);

    }
    void Update()
    {
        // --- Movimiento con el Ratón (para probar en PC) ---
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            Mover(mouseX * velocidadMovimiento);
        }

        // --- Movimiento Táctil (para móvil) ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Mover(touch.deltaPosition.x * 0.1f); 
        }
    }
    void LateUpdate()
    {
        if (nodosDeSeguimiento.Length > 0)
        {
            // El primer nodo (Nodo_0) sigue directamente al PlayerController
            nodosDeSeguimiento[0].position = Vector3.Lerp(
                nodosDeSeguimiento[0].position,
                transform.position,
                velocidadSeguimientoNodos * Time.deltaTime
            );

            // El resto de los nodos se siguen en cadena
            for (int i = 1; i < nodosDeSeguimiento.Length; i++)
            {
                nodosDeSeguimiento[i].position = Vector3.Lerp(
                    nodosDeSeguimiento[i].position,
                    nodosDeSeguimiento[i - 1].position, // Sigue al nodo anterior
                    velocidadSeguimientoNodos * Time.deltaTime
                );
            }
        }
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
                    AñadirHamburguesas(numero);
                }
                else if (tipo == PortalSO.PortalType.Multiplicacion)
                {
                    Debug.Log("Multiplicar las hamburguesas por " + numero + "!");
                    int cantidadActual = contadorVirtualHamburguesas;
                    int cantidadAAnadir = cantidadActual * (numero - 1);
                    AñadirHamburguesas(cantidadAAnadir);
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
                // Desactivamos el collider de detección para no iniciar la batalla múltiples veces.
                other.enabled = false;
            }
        }
    }
    void IniciarBatallaConJefe(JefeController jefe)
    {
        int poderDelJugador = contadorVirtualHamburguesas;
        int vidaDelJefe = jefe.VidaActual;

        if (poderDelJugador >= vidaDelJefe)
        {
            RemoverMultiplesHamburguesas(vidaDelJefe);
            jefe.DerrotarJefe();
            GameManager.Instance.vidaAdicionalParaJefe += 10;
            Debug.Log("¡El próximo jefe será más fuerte! Vida adicional ahora: " + GameManager.Instance.vidaAdicionalParaJefe);
        }
        else
        {
            RemoverMultiplesHamburguesas(poderDelJugador);
            Debug.Log("¡GAME OVER!");
        }
    }
    private void RemoverUnModeloFisico(Hamburguesa hamburguesa)
    {
        if (hamburguesaPool != null && multitudDeHamburguesas.Contains(hamburguesa))
        {
            hamburguesaPool.ReturnObject(hamburguesa);
            multitudDeHamburguesas.Remove(hamburguesa);
        }
    }
    void RemoverMultiplesHamburguesas(int cantidadDePoderAPerder)
    {
        contadorVirtualHamburguesas -= cantidadDePoderAPerder;
        if (contadorVirtualHamburguesas < 0) contadorVirtualHamburguesas = 0;

        int modelosFisicosObjetivo = Mathf.Min(contadorVirtualHamburguesas, 30);

        int modelosARemover = multitudDeHamburguesas.Count - modelosFisicosObjetivo;

        for (int i = 0; i < modelosARemover; i++)
        {
            if (multitudDeHamburguesas.Count > 0)
            {
                RemoverUnModeloFisico(multitudDeHamburguesas[multitudDeHamburguesas.Count - 1]);
            }
        }

        if (uiManager != null)
        {
            uiManager.ActualizarContadorHamburguesas(contadorVirtualHamburguesas);
        }
        if (contadorVirtualHamburguesas <= 0)
        {
            GameManager.Instance.FinDePartida();
        }
    }

    private void AñadirHamburguesas(int cantidad)
    {
        contadorVirtualHamburguesas += cantidad;

        AñadirHamburguesasFisicas(cantidad);

        if (uiManager != null)
        {
            uiManager.ActualizarContadorHamburguesas(contadorVirtualHamburguesas);
        }
    }
    private void AñadirHamburguesasFisicas(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            if (multitudDeHamburguesas.Count >= 30)
            {
                Debug.Log("Límite visual de hamburguesas alcanzado.");
                break;
            }

            Vector3 posicionSpawn = transform.position;
            Hamburguesa nuevaHamburguesa = generador.GenerarHamburguesa(posicionSpawn);

            if (nuevaHamburguesa != null)
            {
                nuevaHamburguesa.crowdController = this;
                multitudDeHamburguesas.Add(nuevaHamburguesa);
            }
            else
            {
                break;
            }
        }
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

            Vector3 posicionObjetivo = transform.position + new Vector3(targetX, 0, targetZ + desplazamientoZFormacion);
            multitudDeHamburguesas[i].MoverAPosicion(posicionObjetivo);
        }
    }
    public void RemoverHamburguesa(Hamburguesa hamburguesa)
    {
        contadorVirtualHamburguesas--; 
        RemoverUnModeloFisico(hamburguesa); 

        if (uiManager != null)
        {
            uiManager.ActualizarContadorHamburguesas(contadorVirtualHamburguesas);
        }
        if (contadorVirtualHamburguesas <= 0)
        {
            GameManager.Instance.FinDePartida();
        }
    }
}
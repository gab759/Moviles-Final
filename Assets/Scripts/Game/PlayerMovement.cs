using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de la Multitud")]
    [SerializeField] private int columnas = 5; 
    [SerializeField] private float distanciaX = 1.2f; 
    [SerializeField] private float distanciaZ = 1.0f; 
    [Header("Dependencias")]
    [SerializeField] private GeneradorDeHamburguesas generador;
    private List<Hamburguesa> multitudDeHamburguesas = new List<Hamburguesa>();
    [Header("Configuración de Movimiento")]
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
            Debug.LogError("El Generador no está asignado en PlayerMovement.", this);
        }

         AñadirHamburguesas(2); 
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
                    int cantidadActual = multitudDeHamburguesas.Count;
                    int cantidadAAnadir = cantidadActual * (numero - 1);
                    AñadirHamburguesas(cantidadAAnadir);
                }

                other.gameObject.SetActive(false);
            }
        }
    }
    private void AñadirHamburguesas(int cantidad)
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
        hamburguesa.gameObject.SetActive(false);

        multitudDeHamburguesas.Remove(hamburguesa);

    }
}
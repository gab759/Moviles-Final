using UnityEngine;

public class Hamburguesa : PoolObject
{
    private Vector3 posicionObjetivo;
    private float velocidadMovimiento = 1f;
    public PlayerMovement crowdController;
    void Update()
    {
        // Se mueve suavemente hacia su posición objetivo en cada frame
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidadMovimiento * Time.deltaTime);
    }

    // El PlayerController llamará a esta función para darle una nueva orden de posición
    public void MoverAPosicion(Vector3 nuevaPosicion)
    {
        this.posicionObjetivo = nuevaPosicion;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // La hamburguesa no se desactiva a sí misma.
            // Simplemente le avisa a su jefe para que él tome las decisiones.
            if (crowdController != null)
            {
                crowdController.RemoverHamburguesa(this);
            }
        }
    }
}

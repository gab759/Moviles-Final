using UnityEngine;

public class Hamburguesa : PoolObject
{
    private Vector3 posicionObjetivo;
    private float velocidadMovimiento = 1f;
    public PlayerMovement crowdController;
    void Update()
    {
        // Se mueve suavemente hacia su posici�n objetivo en cada frame
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidadMovimiento * Time.deltaTime);
    }

    // El PlayerController llamar� a esta funci�n para darle una nueva orden de posici�n
    public void MoverAPosicion(Vector3 nuevaPosicion)
    {
        this.posicionObjetivo = nuevaPosicion;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // La hamburguesa no se desactiva a s� misma.
            // Simplemente le avisa a su jefe para que �l tome las decisiones.
            if (crowdController != null)
            {
                crowdController.RemoverHamburguesa(this);
            }
        }
    }
}
